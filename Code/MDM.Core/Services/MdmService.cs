namespace EnergyTrading.Mdm.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data;
    using EnergyTrading.Extensions;
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Contracts.Rules;
    using EnergyTrading.Mdm.Data;
    using EnergyTrading.Mdm.Data.Search;
    using EnergyTrading.Mdm.Extensions;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    /// <summary>
    /// Create and map a type of MDM entity.
    /// </summary>
    /// <remark>
    /// There is no transaction management in this class as the REST level has this.
    /// However, there is an argument for using TransactionScope on all public methods (low cost as we would enroll in the same trans)
    /// so that we can guarantee integrity at the service level as well.
    /// </remark>
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TMapping">Mapping type</typeparam>
    /// <typeparam name="TDetails">Details type</typeparam>
    /// <typeparam name="TDetailsContract">Details type in the contract</typeparam>
    public abstract class MdmService<TContract, TEntity, TMapping, TDetails, TDetailsContract> : IMdmService<TContract, TEntity>
        where TContract : class, IMdmEntity, new()
        where TEntity : class, IEntity
        where TMapping : class, IEntityMapping, IIdentifiable, new()
        where TDetails : class, IEntityDetail
    {
        private readonly IValidatorEngine validatorEngine;
        private readonly IRepository repository;
        private ISearchCache searchCache;

        protected MdmService(IValidatorEngine validatorEngine, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache)
            : this(validatorEngine, mappingEngine, repository, searchCache, 0)
        {
        }

        protected MdmService(IValidatorEngine validatorEngine, IMappingEngine mappingEngine, IRepository repository, ISearchCache searchCache, uint version)

        {
            this.MappingEngine = mappingEngine;
            this.validatorEngine = validatorEngine;
            this.repository = repository;
            this.searchCache = searchCache;
            this.ContractVersion = version;
        }

        public uint ContractVersion 
        {
            get; private set; 
        }

        protected IMappingEngine MappingEngine { get; private set; }

        /// <summary>
        /// 
        /// Create an entity from a contract.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public TEntity Create(TContract contract)
        {
            // Check it's ok
            this.Validate(contract);

            // Convert and save it
            var entity = this.MappingEngine.Map<TContract, TEntity>(contract);
            this.repository.Add(entity);
            this.repository.Flush();
            searchCache.Clear();

            return entity;
        }

        /// <summary>
        /// Add a mapping to an existing entity.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IEntityMapping CreateMapping(CreateMappingRequest message)
        {
            // Check it's ok
            this.Validate(message);

            // Get a reference to the entity we should operate against
            var entity = this.repository.FindOne<TEntity>(message.EntityId);
            if (entity == null)
            {
                return null;
            }

            // Create the mapping and save
            var mapping = this.MappingEngine.Map<EnergyTrading.Mdm.Contracts.MdmId, TMapping>(message.Mapping);
            entity.ProcessMapping(mapping);

            this.repository.Save(entity);
            this.repository.Flush();
            searchCache.Clear();

            return mapping;
        }

        public void DeleteMapping(DeleteMappingRequest message)
        {
            if (message == null)
            {
                return;
            }

            this.repository.Delete<TMapping>(this.repository.FindOne<TMapping>(message.MappingId));
            this.repository.Flush();
            searchCache.Clear();
        }

        /// <summary>
        /// Cross map from the source system to the target system for an MDM entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ContractResponse<MappingResponse> CrossMap(CrossMappingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var mapping = this.repository.FindAllMappings<TMapping>(request).FirstOrDefault(m => m.Entity is TEntity);
            return mapping == null 
                 ? this.ConstructResponse(null, request) 
                 : this.ConstructResponse((TEntity)mapping.Entity, request);
        }

        /// <summary>
        /// Delivery a collection of data contracts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TContract> List()
        {
            var list = new List<TContract>();
            var now = SystemTime.UtcNow();

            foreach (var entity in this.repository.Queryable<TEntity>().ToList().Where(x => x.Validity.Start <= now && x.Validity.Finish >= now))
            {
                // TODO: Constrain the identifiers we retrieve or have an enum to allow this e.g. Nexus, Originating, Default, All
                list.Add(this.ConstructContract(entity, now));
            }

            return list;
        }

        /// <summary>
        /// Delivery a collection of data contracts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TContract> EntityList(GetRequest request)
        {
            var list = new List<TContract>();

            var entity = this.repository.FindOne<TEntity>(request.EntityId);

            if (entity != null)
            {
                list.AddRange(this.Details(entity).Select(detail => this.ConstructContract(entity, detail.Validity.Start)));
            }

            return list;
        }

        /// <summary>
        /// Delivery an MDM entity as a data contract for a mapping request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ContractResponse<TContract> Map(MappingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.IsNexusMappingRequest())
            {
                if (request.HasNumericIdentifier())
                {
                    return this.Request(request.ToGetRequest());
                }
                else
                {
                    var response = this.ContractResponse(null, request.ValidAt, request.Version);
                    response.Error.Reason = ErrorReason.Identifier;
                    return response;
                }
            }

            this.Validate(request);

            var mapping = this.repository.FindAllMappings<TMapping>(request).FirstOrDefault(m => m.Entity is TEntity);
            return mapping == null
                 ? this.ContractResponse(null, request.ValidAt, request.Version) 
                 : this.ContractResponse(mapping.Entity as TEntity, request.ValidAt, request.Version);
        }

        /// <summary>
        /// Deliver an MDM entity as a data contract as of a point in time
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ContractResponse<TContract> Request(GetRequest request)
        {
            var entity = this.repository.FindOne<TEntity>(request.EntityId);
            return this.ContractResponse(entity, request.ValidAt, request.Version);
        }

        /// <summary>
        /// Get a particular mapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ContractResponse<MappingResponse> RequestMapping(GetMappingRequest request)
        {
            // Get the entity
            var mapping = this.repository.FindOne<TMapping>(request.MappingId);
            if (mapping == null || request.EntityId != mapping.Entity.Id)
            {
                return new ContractResponse<MappingResponse>
                {
                    Error = new ContractError
                    {
                        Type = ErrorType.NotFound
                    },
                    IsValid = false
                };
            }
            if (mapping.Entity.Id != request.EntityId)
            {
                return new ContractResponse<MappingResponse>
                {
                    Error = new ContractError
                    {
                        Type = ErrorType.NotFound
                    },
                    IsValid = false
                }; 
            }

            var mr = new MappingResponse();
            mr.Mappings.Add(this.MappingEngine.Map<TMapping, MdmId>(mapping));
            var response = new ContractResponse<MappingResponse>
            {
                Contract = mr,
                Version = mapping.Entity.Version,
                IsValid = true
            };

            return response;
        }

        /// <summary>
        /// Amend an entity based on a contract
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="contract"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public ContractResponse<TContract> Update(int entityId, ulong version, TContract contract)
        {
            // Get the entity
            var entity = this.repository.FindOne<TEntity>(entityId);
            if (entity == null)
            {
                return new ContractResponse<TContract>()
                {
                    Error = new ContractError
                    {
                        Type = ErrorType.NotFound,
                        Reason = ErrorReason.Identifier
                    },
                    IsValid = false
                };
            }

            if (entity.Version != version)
            {
                throw new VersionConflictException();
            }

            // Check it's ok
            this.Validate(contract);

            // Handle each identifier individually - if a nexus id has been passed in then it should be ignored            
            foreach (var identifier in contract.Identifiers.Where(id => id.SystemName != MdmInternalName.Name))
            {
                var mapping = this.MappingEngine.Map<EnergyTrading.Mdm.Contracts.MdmId, TMapping>(identifier);
                entity.ProcessMapping(mapping);
            }

            // Convert and save it
            var details = this.MappingEngine.Map<TDetailsContract, TDetails>((TDetailsContract)contract.Details);
            if (contract.MdmSystemData != null)
            {
                details.Validity = this.MappingEngine.Map<SystemData, EnergyTrading.DateRange>(contract.MdmSystemData);
            }
            entity.AddDetails(details);

            this.repository.Save(entity);
            this.repository.Flush();
            searchCache.Clear();

            // Use the details we've just added as the as of
            return this.ContractResponse(entity, details.Validity.Start, 0);
        }

        /// <summary>
        /// 
        /// Update an existing mapping for an entity.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IEntityMapping UpdateMapping(AmendMappingRequest message)
        {
            // Raise validation exception if message request is null
            if (message ==null)
            {
                throw new ValidationException(new List<IRule>() { new PredicateRule<AmendMappingRequest>(p => p != null, "AmendMappingRequest should not be null.") });
            }

            // Raise validation exception if message request is null TODO: this should really be in a validator, but existing validator checks overlaps and is therefore after the look up below
            if (message == null)
            {
                throw new ValidationException(new List<IRule>() { new PredicateRule<AmendMappingRequest>(p => p != null, "AmendMappingRequest should not be null.") });
            }
            // Raise validation exception if mapping is null TODO: this should really be in a validator
            if (message.Mapping == null)
            {
                throw new ValidationException(new List<IRule>() { new PredicateRule<AmendMappingRequest>(p => p.Mapping != null, "Mapping should not be null.") });
            }
            // Raise validation exception if mapping system name is null TODO: this should really be in a validator
            if (message.Mapping.SystemName == null)
            {
                throw new ValidationException(new List<IRule>() { new PredicateRule<AmendMappingRequest>(p => !string.IsNullOrWhiteSpace(p.Mapping.SystemName), "Mapping System Name must not be null or an empty string") });
            }

            // Get a reference to the entity we should operate against
            var mapping = this.repository.FindOne<TMapping>(message.MappingId);
            if (mapping == null)
            {
                return null;
            }

            // NB Should we be comparing with the mapping version or the entity version?
            if (mapping.Entity.Version != message.Version)
            {
                throw new VersionConflictException();
            }

            // Check it's ok
            this.Validate(message);

            // Ensure the id is on the message
            message.Mapping.MappingId = message.MappingId;

            // Translate from the contract
            var changedMapping = this.MappingEngine.Map<MdmId, TMapping>(message.Mapping);

            // Update the mapping and save
            var entity = (TEntity)mapping.Entity;
            entity.ProcessMapping(changedMapping);

            this.repository.Save(entity);
            this.repository.Flush();
            searchCache.Clear();

            return mapping;
        }

        public SearchResultPage<TContract> GetSearchResults(string searchKey, int pageNumber)
        {
            var cacheSearchResultPage = this.searchCache.Get(searchKey, pageNumber);

            if (cacheSearchResultPage == null)
            {
                return null;
            }

            var entities = this.repository.Queryable<TEntity>().Include("Mappings").Where(x => cacheSearchResultPage.EntityIds.Contains(x.Id)).ToList();

            var orderedEntities = InSameOrderAs(entities, cacheSearchResultPage.EntityIds);

            var searchResultPage = new SearchResultPage<TContract>(
                cacheSearchResultPage, 
                orderedEntities.Select(entity => this.ConstructContract(entity, cacheSearchResultPage.AsOf)).ToList());

            return searchResultPage;
        }

        public SearchResultPage<TContract> CreateNonTemporalSearch(Search search)
        {
            if (search.AsOf == null)
            {
                search.AsOf = SystemTime.UtcNow();
            }

            var searchService = new SearchService<TEntity, TDetails, TMapping>(new QueryFactory(), new SearchCommand<TEntity, TDetails, TMapping>(this.repository));
            var results = searchService.NonTemporalSearch(search);

            var searchResultPage = new SearchResultPage<TContract>(results.Select(entity => this.ConstructContract(entity, search.AsOf.Value)).ToList(), search.AsOf.Value);

            return searchResultPage;
        }

        public SearchResultPage<TContract> CreateDirectSearch(Search search, IEntitySearchCommand<TEntity> searchCommand)
        {
            if (search.AsOf == null)
            {
                search.AsOf = SystemTime.UtcNow();
            }

            var queryFactory = new QueryFactory();
            var query = queryFactory.CreateQuery(search);
            var results = searchCommand.Execute(this.repository, query == string.Empty ? "true" : query, search.AsOf, search.SearchOptions.ResultsPerPage, search.SearchOptions.OrderBy, search);
            var searchResultPage = new SearchResultPage<TContract>(results.Select(entity => this.ConstructContract(entity, search.AsOf.Value)).ToList(), search.AsOf.Value);

            return searchResultPage;
        }

        public string CreateSearch(Search search)
        {
            var searchService = new SearchService<TEntity, TDetails, TMapping>(new QueryFactory(), new SearchCommand<TEntity, TDetails, TMapping>(this.repository));
            var entityIds = searchService.Search(search);

            if (entityIds.Any())
            {
                var searchKey = search.ToKey<TContract>(ContractVersion);
                this.searchCache.Add(searchKey, new SearchResult(entityIds, search.AsOf ?? SystemTime.UtcNow(), search.SearchOptions.MultiPage, search.SearchOptions.ResultsPerPage));

                return searchKey;
            }

            return null;
        }

        protected abstract IEnumerable<TDetails> Details(TEntity entity);

        protected abstract IEnumerable<TMapping> Mappings(TEntity entity);

        // TODO: This will be replace by a mapper. More to do.
        protected void PopulateEntityLevelAttributes(TContract contract, TEntity entity)
        {
            this.MappingEngine.Map<TEntity, TContract>(entity, contract);
        }

        private static IEnumerable<T> InSameOrderAs<T>(IEnumerable<T> entities, IEnumerable<int> entityIds)
            where T : class, IEntity
        {
            var dictionary = entities.ToDictionary(x => x.Id);

            return entityIds.Where(dictionary.ContainsKey)
                .Select(x => dictionary[x]);
        }


        private void AssignDetails(TContract contract, TEntity entity, Func<TDetails, bool> validator)
        {
            var details = this.Details(entity).Where(validator).FirstOrDefault();
            var target = this.MappingEngine.Map<TDetails, TDetailsContract>(details);
            contract.Details = target;
            contract.MdmSystemData = new SystemData { StartDate = details.Validity.Start, EndDate = details.Validity.Finish };
        }

        private void AssignIdentifiers(TEntity entity, Action<EnergyTrading.Mdm.Contracts.MdmId> assigner, Func<TMapping, bool> validator)
        {
            foreach (var candidate in this.Mappings(entity).Where(validator))
            {
                var target = this.MappingEngine.Map<TMapping, MdmId>(candidate);
                assigner(target);
            }
        }

        private void AssignLinks(TContract contract, TEntity entity)
        {
            var target = this.MappingEngine.Map<TEntity, List<EnergyTrading.Contracts.Atom.Link>>(entity);
            contract.Links = target;
        }

        private void AssignAudit(TContract contract, TEntity entity)
        {
            contract.Audit = new Audit { Version = entity.Version};
        }

        private TContract ConstructContract(TEntity entity, DateTime validAt)
        {
            var contract = new TContract();
            contract.Identifiers.Add(entity.CreateNexusMapping());
            this.AssignIdentifiers(entity, x => contract.Identifiers.Add(x), x => x.Validity.ValidAt(validAt));
            this.AssignDetails(contract, entity, x => x.Validity.ValidAt(validAt));
            this.AssignLinks(contract, entity);
            this.AssignAudit(contract, entity);
            this.PopulateEntityLevelAttributes(contract, entity);
            return contract;
        }

        /// <summary>
        /// Produce the contract response for the entity at the specified time.
        /// <para>
        /// Does not populate the contract if the entity version is the same as the requested one or the entity has been expired (validate against the validAt)
        /// </para>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="validAt"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private ContractResponse<TContract> ContractResponse(TEntity entity, DateTime validAt, ulong version)
        {
            // If details are null, e.g: entity has been expired (filter the details based on the validAt date)
            // then return EntityNotFound message
            if (entity != null && this.Details(entity).FirstOrDefault(x => x.Validity.ValidAt(validAt)) != null)
            {
                return new ContractResponse<TContract>
                {
                    // NB Safest is to do equality rather than > since entity.Version can be a large negative number
                    // and version will default to 0 if not provided.
                    Contract = entity.Version == version ? null : this.ConstructContract(entity, validAt),
                    Version = entity.Version,
                };
            }

            return new ContractResponse<TContract>
            {
                Error = new ContractError
                {
                    Type = ErrorType.NotFound
                },
                IsValid = false
            };
        }

        private ContractResponse<MappingResponse> ConstructResponse(TEntity entity, CrossMappingRequest request)
        {
            if (entity != null)
            {
                var response = new MappingResponse();
                this.AssignIdentifiers(
                    entity,
                    x => response.Mappings.Add(x),
                    x =>
                    string.Equals(x.System.Name, request.TargetSystemName, StringComparison.InvariantCultureIgnoreCase) &&
                    x.Validity.ValidAt(request.ValidAt));

                if (response.HasMultipleDefaultMapping() || response.HasMultipleMappingsWithNoDefault())
                {
                    return new ContractResponse<MappingResponse>
                        {
                            Error = new ContractError { Type = ErrorType.Ambiguous }, IsValid = false 
                        };
                }

                if (response.HasMutlipleMappingsWithOneDefault())
                {
                    response.Mappings = new MdmIdList()
                        {
                            response.Mappings.Where(x => x.DefaultReverseInd.HasValue && x.DefaultReverseInd.Value).
                                First()
                        };
                }

                return new ContractResponse<MappingResponse>
                    {
                        Contract = request.Version == entity.Version ? null : response, Version = entity.Version, 
                    };
            }

            return new ContractResponse<MappingResponse>
                {
                    Error = new ContractError { Type = ErrorType.NotFound }, IsValid = false 
                };
        }

        private void Validate<T>(T value)
        {
            var errors = new List<IRule>();
            if (!this.validatorEngine.IsValid(value, errors))
            {
                throw new ValidationException(errors);
            }
        }
    }
}
namespace EnergyTrading.MDM.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;
    using EnergyTrading.Search;
    using EnergyTrading.Validation;

    using DateRange = EnergyTrading.DateRange;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    [TestClass]
    public class MdmServiceFixture
    {
        [TestMethod]
        public void ShouldGetEntityByIdWhenGetRequestRequested()
        {
            // Given
            var mockValidationEngine = new Mock<IValidatorEngine>();
            var mockMappingEngine = new Mock<IMappingEngine>();
            var mockRepository = new Mock<IRepository>();
            var mockSearchCache = new Mock<ISearchCache>();
            var abcService = new AbcEntityTestService(mockValidationEngine.Object, mockMappingEngine.Object, mockRepository.Object, mockSearchCache.Object);

            // When
            var getRequest = new GetRequest() { EntityId = 3, ValidAt = DateTime.Now };
            abcService.Request(getRequest);

            // Then
            mockRepository.Verify(x => x.FindOne<AbcEntity>(getRequest.EntityId));
        }

        [TestMethod]
        public void ShouldFindMappingWhenMapRequestedForNonNexusSystems()
        {
            // Given
            var mockValidationEngine = new Mock<IValidatorEngine>();
            var mockMappingEngine = new Mock<IMappingEngine>();
            var mockRepository = new Mock<IRepository>();
            var mockSearchCache = new Mock<ISearchCache>();
            IQueryable<AbcEntityMapping> abcEntityMappingQueryObject =
                (new List<AbcEntityMapping>
                    {
                        new AbcEntityMapping()
                            {
                                System = new SourceSystem() { Name = "Endur" },
                                MappingValue = "EndurId-3",
                                Validity = new DateRange(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1))
                            }
                    }).AsQueryable();
            var abcService = new AbcEntityTestService(
                mockValidationEngine.Object, mockMappingEngine.Object, mockRepository.Object, mockSearchCache.Object);

            // When
            var mappingRequest = new MappingRequest()
                {
                    Identifier = "EndurId-3",
                    SystemName = "Endur",
                    ValidAt = DateTime.Now
                };
            mockValidationEngine.Setup(x => x.IsValid<MappingRequest>(mappingRequest, It.IsAny<IList<IRule>>())).Returns(true);
            mockRepository.Setup(x => x.Queryable<AbcEntityMapping>()).Returns(abcEntityMappingQueryObject);

            abcService.Map(mappingRequest);

            // Then
            mockRepository.Verify(x => x.Queryable<AbcEntityMapping>());
        }

        [TestMethod]
        public void ShouldGetEntityByIdWhenMapRequestedForNexusSystem()
        {
            // Given
            var mockValidationEngine = new Mock<IValidatorEngine>();
            var mockMappingEngine = new Mock<IMappingEngine>();
            var mockRepository = new Mock<IRepository>();
            var mockSearchCache = new Mock<ISearchCache>();
            var abcService = new AbcEntityTestService(mockValidationEngine.Object, mockMappingEngine.Object, mockRepository.Object, mockSearchCache.Object);

            // When
            var mappingRequest = new MappingRequest()
            {
                Identifier = "3",
                SystemName = "Nexus",
                ValidAt = DateTime.Now
            };

            abcService.Map(mappingRequest);

            // Then
            mockRepository.Verify(x => x.FindOne<AbcEntity>(int.Parse(mappingRequest.Identifier)));
        }

        [TestMethod]
        public void ShouldReturnNotFoundWhenMapRequestedWithInvalidNexusIdentifier()
        {
            // Given
            var mockValidationEngine = new Mock<IValidatorEngine>();
            var mockMappingEngine = new Mock<IMappingEngine>();
            var mockRepository = new Mock<IRepository>();
            var mockSearchCache = new Mock<ISearchCache>();
            var abcService = new AbcEntityTestService(mockValidationEngine.Object, mockMappingEngine.Object, mockRepository.Object, mockSearchCache.Object);

            // When
            var mappingRequest = new MappingRequest()
            {
                Identifier = "Three",
                SystemName = "Nexus",
                ValidAt = DateTime.Now
            };

            var response = abcService.Map(mappingRequest);

            Assert.AreEqual(ErrorType.NotFound, response.Error.Type);
        }

        [TestMethod]
        public void ShouldReturnSearchResultsFromCache()
        {
            // Given
            var mockValidationEngine = new Mock<IValidatorEngine>();
            var mockMappingEngine = new Mock<IMappingEngine>();
            var mockRepository = new Mock<IRepository>();
            var mockSearchCache = new Mock<ISearchCache>();
            var abcService = new AbcEntityTestService(mockValidationEngine.Object, mockMappingEngine.Object, mockRepository.Object, mockSearchCache.Object);

            // When
            var ids = new List<int> {1, 2, 3};
            var cacheSearchResultsPage = new CacheSearchResultPage(ids, DateTime.Now, 2, "abc");
            mockSearchCache.Setup(x => x.Get("abc", 1)).Returns(cacheSearchResultsPage);

            var entities = new List<AbcEntity>
                               {
                                   new AbcEntity {Id = 1, Validity = new DateRange()},
                                   new AbcEntity {Id = 2, Validity = new DateRange()},
                                   new AbcEntity {Id = 3, Validity = new DateRange()}
                               };
            
            var details = new AbcEntityDetails { Validity = new DateRange() };
            foreach (var abcEntity in entities)
            {
                abcEntity.AddDetails(details);
            }

            mockRepository.Setup(x => x.Queryable<AbcEntity>()).Returns(entities.AsQueryable());

            var searchResultsPage = abcService.GetSearchResults("abc", 1);

            Assert.AreEqual("abc", searchResultsPage.SearchResultsKey);
            Assert.AreEqual(2, searchResultsPage.NextPage);
            Assert.AreEqual(3, searchResultsPage.Contracts.Count);
        }

        [TestMethod]
        public void ShouldReturnOrderedSearchResultsFromCache()
        {
            // Given
            var mockValidationEngine = new Mock<IValidatorEngine>();
            var mockMappingEngine = new Mock<IMappingEngine>();
            var mockRepository = new Mock<IRepository>();
            var mockSearchCache = new Mock<ISearchCache>();
            var abcService = new AbcEntityTestService(mockValidationEngine.Object, mockMappingEngine.Object, mockRepository.Object, mockSearchCache.Object);

            // When

            // ids are stored in a specific order
            var ids = new List<int> { 1, 100, 50 };
            var cacheSearchResultsPage = new CacheSearchResultPage(ids, DateTime.Now, 2, "abc");
            mockSearchCache.Setup(x => x.Get("abc", 1)).Returns(cacheSearchResultsPage);

            // entities are returned in a different order
            var entities = new List<AbcEntity>
                               {
                                   new AbcEntity {Id = 100, Validity = new DateRange()},
                                   new AbcEntity {Id = 50, Validity = new DateRange()},
                                   new AbcEntity {Id = 1, Validity = new DateRange()}
                               };
            
            var details = new AbcEntityDetails { Validity = new DateRange() };
            foreach (var abcEntity in entities)
            {
                abcEntity.AddDetails(details);
            }

            mockRepository.Setup(x => x.Queryable<AbcEntity>()).Returns(entities.AsQueryable());

            var searchResultsPage = abcService.GetSearchResults("abc", 1);

            Assert.AreEqual("abc", searchResultsPage.SearchResultsKey);
            Assert.AreEqual(2, searchResultsPage.NextPage);
            Assert.AreEqual(3, searchResultsPage.Contracts.Count);
            Assert.AreEqual(1, searchResultsPage.Contracts[0].ToNexusKey());
            Assert.AreEqual(100, searchResultsPage.Contracts[1].ToNexusKey());
            Assert.AreEqual(50, searchResultsPage.Contracts[2].ToNexusKey());
        }
    }

    internal class AbcEntityTestService :
            MdmService<AbcEntityContract, AbcEntity, AbcEntityMapping, AbcEntityDetails, AbcEntityDetailsContract>
    {
        public AbcEntityTestService(
            IValidatorEngine validatorEngine,
            IMappingEngine mappingEngine,
            IRepository repository,
            ISearchCache searchCache)
            : base(validatorEngine, mappingEngine, repository, searchCache)
        {
        }

        protected override IEnumerable<AbcEntityDetails> Details(AbcEntity entity)
        {
            return entity.Details;
        }

        protected override IEnumerable<AbcEntityMapping> Mappings(AbcEntity entity)
        {
            return Enumerable.Empty<AbcEntityMapping>();
        }
    }

    internal class AbcEntity : IEntity
    {
        public AbcEntity()
        {
            this.Details = new List<AbcEntityDetails>();
        }

        public int Id { get; set; }

        public DateRange Validity { get; set; }

        public ulong Version { get; set; }

        public IList<AbcEntityDetails> Details { get; private set; }


        public void AddDetails(IEntityDetail details)
        {
            this.Details.Add(details as AbcEntityDetails);
        }

        public void ProcessMapping(IEntityMapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class AbcEntityDetails : IEntityDetail
    {
        public DateRange Validity { get; set; }

        public IEntity Entity { get; set; }

        public ulong Version { get; set; }

        public int Id { get; set; }
    }

    internal class AbcEntityMapping : IEntityMapping
    {
        public object Id { get; set; }

        public DateRange Validity { get; set; }

        public IEntity Entity { get; set; }

        public ulong Version { get; set; }

        public int MappingId { get; set; }

        public SourceSystem System { get; set; }

        public string MappingValue { get; set; }

        public bool IsMaster { get; set; }

        public bool IsDefault { get; set; }

        public void ChangeEndDate(DateTime value)
        {
            throw new NotImplementedException();
        }
    }

    internal class AbcEntityContract : IMdmEntity
    {
        public AbcEntityContract()
        {
            Identifiers = new NexusIdList();
        }

        public NexusIdList Identifiers { get; set; }

        public object Details { get; set; }

        public SystemData Nexus { get; set; }

        public Audit Audit { get; set; }

        public List<RWEST.Nexus.Contracts.Atom.Link> Links { get; set; }
    }

    internal class AbcEntityDetailsContract
    {
        public string Name { get; set; }
    }

}
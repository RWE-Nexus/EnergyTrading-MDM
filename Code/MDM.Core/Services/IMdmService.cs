namespace EnergyTrading.Mdm.Services
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Data.Search;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Search;

    /// <summary>
    /// Create and map a type of MDM entity.
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IMdmService<TContract, TEntity>
    {
        /// <summary>
        /// Validate the contract and create an entity.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        TEntity Create(TContract contract);

        /// <summary>
        /// Add a mapping to an existing entity.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IEntityMapping CreateMapping(CreateMappingRequest message);

        /// <summary>
        /// Cross map from the source system to the target system for an MDM entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ContractResponse<MappingResponse> CrossMap(CrossMappingRequest request);

        /// <summary>
        /// Delivery a collection of data contracts
        /// </summary>
        /// <returns></returns>
        IEnumerable<TContract> List();

        /// <summary>
        /// Delivery an MDM entity as a data contract for a mapping request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ContractResponse<TContract> Map(MappingRequest request);

        /// <summary>
        /// Deliver an MDM entity as a data contract as of a point in time
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ContractResponse<TContract> Request(GetRequest request);

        /// <summary>
        /// Get a particular mapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ContractResponse<MappingResponse> RequestMapping(GetMappingRequest request);

        /// <summary>
        /// Validate the contract and amend an entity.
        /// </summary>
        /// <param name="entityId">Identity of the entity.</param>
        /// <param name="version">The version we are intending to update</param>
        /// <param name="contract"></param>
        /// <returns></returns>
        ContractResponse<TContract> Update(int entityId, ulong version, TContract contract);

        /// <summary>
        /// Delete a mapping from an existing entit
        /// </summary>
        /// <param name="message"></param>
        void DeleteMapping(DeleteMappingRequest message);

        /// <summary>
        /// Update an existing mapping for an entity.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IEntityMapping UpdateMapping(AmendMappingRequest message);

        /// <summary>
        /// Create
        /// Search for a contract
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        string CreateSearch(Search search);

        /// <summary>
        /// Get the results of a search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        SearchResultPage<TContract> GetSearchResults(string searchKey, int pageNumber);

        SearchResultPage<TContract> CreateNonTemporalSearch(Search search);

        SearchResultPage<TContract> CreateDirectSearch(Search search, IEntitySearchCommand<TEntity> searchCommand);

        /// <summary>
        /// Delivery a collection of data contracts
        /// </summary>
        /// <returns></returns>
        IEnumerable<TContract> EntityList(GetRequest request);

        /// <summary>
        /// The version of the contract / entity this service supports
        /// </summary>
        uint ContractVersion { get; }
    }
}
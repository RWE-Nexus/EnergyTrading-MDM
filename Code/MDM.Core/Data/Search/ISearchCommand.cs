namespace EnergyTrading.MDM.Data.Search
{
    using System;
    using System.Collections.Generic;

    public interface ISearchCommand<TEntity, TDetails, TMapping>
        where TEntity : class, IEntity where TDetails : class, IEntityDetail where TMapping : class, IEntityMapping
    {
        IList<int> Execute(string query, DateTime? at, int? maxResults, string orderBy);

        IList<TEntity> ExecuteNonTemporal(string query, DateTime? at, int? maxResults, string orderBy);

        IList<int> ExecuteMappingSearch(string query, DateTime? at, int? maxResults);
    }
}
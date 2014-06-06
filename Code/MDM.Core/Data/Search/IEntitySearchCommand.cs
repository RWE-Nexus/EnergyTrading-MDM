namespace EnergyTrading.Mdm.Data.Search
{
    using System;
    using System.Collections.Generic;
    using EnergyTrading.Data;

    public interface IEntitySearchCommand<TEntity>
    {
        ICollection<TEntity> Execute(IRepository repository, string query, DateTime? at, int? maxResults, string orderBy, EnergyTrading.Contracts.Search.Search search = null);
    }
}
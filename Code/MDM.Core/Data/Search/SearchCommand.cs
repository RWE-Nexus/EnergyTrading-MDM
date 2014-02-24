namespace EnergyTrading.MDM.Data.Search
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    public class SearchCommand<TEntity, TDetails, TMapping> : ISearchCommand<TEntity, TDetails, TMapping>
        where TDetails : class, IEntityDetail where TEntity : class, IEntity where TMapping : class, IEntityMapping
    {
        private readonly IRepository repository;

        public SearchCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public IList<int> Execute(string query, DateTime? at, int? maxResults, string orderBy)
        {
            try
            {
                IQueryable<TDetails> detailsQuery = this.DetailsQuery(query, at, orderBy);

                if (maxResults.HasValue)
                {
                    detailsQuery = detailsQuery.Take(maxResults.Value);
                }

                List<TDetails> details = detailsQuery.ToList();
                return details.Select(x => x.Entity.Id).ToList();
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }

        public IList<TEntity> ExecuteNonTemporal(string query, DateTime? at, int? maxResults, string orderBy)
        {
            try
            {
                IQueryable<TEntity> entityQuery = this.EntityQuery(query, at, orderBy);

                if (maxResults.HasValue)
                {
                    entityQuery = entityQuery.Take(maxResults.Value);
                }

                return entityQuery.ToList();
            }
            catch (Exception)
            {
                return new List<TEntity>();
            }
        }

        public IList<int> ExecuteMappingSearch(string query, DateTime? at, int? maxResults)
        {
            // There is no value in searching all mappings and returning all entities. So we return an empty collection because a mapping string or source
            // system weren't specified
            if (query == "true")
            {
                return new List<int>();
            }

            DateTime asof = at ?? SystemTime.UtcNow();

            try
            {
                List<TMapping> mappings = this.repository.Queryable<TMapping>().Where(query).
                    Where(x => x.Validity.Start <= asof && x.Validity.Finish  >= asof).ToList();

                var allEntityIds = mappings.Select(mapping => mapping.Entity.Id); 

                IList<int> entityIds = (!maxResults.HasValue ? allEntityIds : allEntityIds.Take(maxResults.Value)).ToList();

                return this.repository.Queryable<TEntity>().Where(x => entityIds.Contains(x.Id)).Select(x => x.Id).ToList();
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }

        private IQueryable<TEntity> EntityQuery(string query, DateTime? at, string orderBy)
        {
            DateTime asof = at ?? SystemTime.UtcNow();

            var queryable = this.repository.Queryable<TEntity>()
                .Include("Mappings")
                .Where(x => x.Validity.Start <= asof && x.Validity.Finish >= asof)
                .Where(query);

            if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }

            return queryable;

        }

        private IQueryable<TDetails> DetailsQuery(string query, DateTime? at, string orderBy)
        {
            DateTime asof = at ?? SystemTime.UtcNow();

            var queryable = this.repository.Queryable<TDetails>()
                .Where(x => x.Validity.Start <= asof && x.Validity.Finish >= asof)
                .Where(query);

            if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }

            return queryable;
        }
    }
}
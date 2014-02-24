namespace EnergyTrading.MDM.Data.Search
{
    using System.Collections.Generic;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data;

    public class SearchService<TEntity, TDetails, TMapping>
        where TDetails : class, IEntityDetail where TEntity : class, IEntity where TMapping : class, IEntityMapping
    {
        private readonly IQueryFactory queryFactory;

        private readonly ISearchCommand<TEntity, TDetails, TMapping> searchCommand;

        public SearchService(IQueryFactory queryFactory, ISearchCommand<TEntity, TDetails, TMapping> searchCommand)
        {
            this.queryFactory = queryFactory;
            this.searchCommand = searchCommand;
        }

        public IList<int> Search(Search search)
        {
            int? maxResults = !search.SearchOptions.MultiPage ? search.SearchOptions.ResultsPerPage : null;

            string query = this.queryFactory.CreateQuery(search);

            if (query == string.Empty)
            {
                query = "true";
            }

            return search.SearchOptions.IsMappingSearch ? this.searchCommand.ExecuteMappingSearch(query, search.AsOf, maxResults) :
                this.searchCommand.Execute(query, search.AsOf, maxResults, search.SearchOptions.OrderBy);
        }

        public IList<TEntity> NonTemporalSearch(Search search)
        {
            int? maxResults = !search.SearchOptions.MultiPage ? search.SearchOptions.ResultsPerPage : null;

            string query = this.queryFactory.CreateQuery(search);

            if (query == string.Empty)
            {
                query = "true";
            }

            return this.searchCommand.ExecuteNonTemporal(query, search.AsOf, maxResults, search.SearchOptions.OrderBy);
        }
    }
}
namespace EnergyTrading.MDM.Data.Search
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;

    using EnergyTrading;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Extensions;

    using BrokerRate = EnergyTrading.MDM.BrokerRate;
    using BrokerRateDetails = EnergyTrading.MDM.BrokerRateDetails;

    public class BrokerRateSearchCommand : IEntitySearchCommand<BrokerRate>
    {
        public ICollection<BrokerRate> Execute(IRepository repository, string query, DateTime? at, int? maxResults, string orderBy, Search search)
        {
            try
            {
                IQueryable<BrokerRate> entityQuery = this.BrokerRateQuery(repository, query, at, orderBy, search);

                if (maxResults.HasValue)
                {
                    entityQuery = entityQuery.Take(maxResults.Value);
                }
                List<BrokerRate> details = entityQuery.ToList();

                return details;
            }
            catch (Exception)
            {
                return new List<BrokerRate>();
            }
        }

        private IQueryable<BrokerRate> BrokerRateQuery(IRepository repository, string query, DateTime? at, string orderBy, Search search)
        {
            DateTime asof = at ?? SystemTime.UtcNow();

            var queryable = repository.Queryable<BrokerRateDetails>().Include(x => x.Broker)
                .Where(x => x.Validity.Start <= asof && x.Validity.Finish >= asof);

            // As we don't have name poperty on temporal entities, we cannot navigate like Broker.Name,
            // So we need to query the internal details
            var brokerNameValues = search.SearchFields.Criterias
                .SelectMany(x => x.Criteria.Where(y => y.Field.ToLower() == "broker.name" || y.Field.ToLower() == "brokername"))
                .Select(x=>x.ComparisonValue).ToList();

            if (brokerNameValues.Any())
            {
                // Add query predicate
                queryable = queryable.Where(b => b.Broker.Details.Any(bd => brokerNameValues.Any(x => bd.Name.Contains(x))));

                // Remove the criteria related broker name
                foreach (var serchCriteria in search.SearchFields.Criterias)
                {
                    if (serchCriteria.Criteria != null)
                    {
                        serchCriteria.Criteria.RemoveAll(y => y.Field.ToLower() == "broker.name" || y.Field.ToLower() == "brokername");
                    }
                }
            }

            var deskNameValues = search.SearchFields.Criterias
                .SelectMany(x => x.Criteria.Where(y => y.Field.ToLower() == "desk.name" || y.Field.ToLower() == "deskname"))
                .Select(x=>x.ComparisonValue).ToList();

            if (deskNameValues.Any())
            {
                // Add query predicate
                queryable = queryable.Where(b => b.Desk.Details.Any(bd => deskNameValues.Any(x => bd.Name.Contains(x))));

                // Remove the criteria related broker name
                foreach (var serchCriteria in search.SearchFields.Criterias)
                {
                    if (serchCriteria.Criteria != null)
                    {
                        serchCriteria.Criteria.RemoveAll(y => y.Field.ToLower() == "desk.name" || y.Field.ToLower() == "deskname");
                    }
                }
            }

            var locationNameValues = search.SearchFields.Criterias
                .SelectMany(x => x.Criteria.Where(y => y.Field.ToLower() == "location.name" || y.Field.ToLower() == "locationname"))
                .Select(x=>x.ComparisonValue).ToList();

            if (locationNameValues.Any())
            {
                // Add query predicate
                queryable = queryable.Where(b => locationNameValues.Any(x => b.Location.Name.Contains(x)));

                // Remove the criteria related broker name
                foreach (var serchCriteria in search.SearchFields.Criterias)
                {
                    if (serchCriteria.Criteria != null)
                    {
                        serchCriteria.Criteria.RemoveAll(y => y.Field.ToLower() == "location.name" || y.Field.ToLower() == "locationname");
                    }
                }
            }

            var queryFactory = new QueryFactory();
            query = queryFactory.CreateQuery(search);
            query = string.IsNullOrWhiteSpace(query) ? query : query.Replace("()", string.Empty);

            queryable = queryable.Where(query == string.Empty ? "true" : query);

            if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }

            return queryable.Select(x => x.BrokerRate).Include("Mappings")
                .Include(y => y.Details);
        }
    }
}
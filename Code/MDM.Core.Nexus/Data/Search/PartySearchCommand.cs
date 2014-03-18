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

    public class PartySearchCommand : IEntitySearchCommand<Party>
    {
        public ICollection<Party> Execute(IRepository repository, string query, DateTime? at, int? maxResults, string orderBy, EnergyTrading.Contracts.Search.Search search)
        {
            try
            {
                IQueryable<Party> entityQuery = this.UnlimitedDetailTemporalQuery(repository, query, at, orderBy);

                if (maxResults.HasValue)
                {
                    entityQuery = entityQuery.Take(maxResults.Value);
                }
                List<Party> details = entityQuery.ToList();

                return details;
            }
            catch (Exception)
            {
                return new List<Party>();
            }
        }

        private IQueryable<Party> UnlimitedDetailTemporalQuery(IRepository repository, string query, DateTime? at, string orderBy)
        {
            DateTime asof = at ?? SystemTime.UtcNow();

            var queryable = repository.Queryable<PartyDetails>()
                .Include("Party")
                .Where(x => x.Validity.Start <= asof && x.Validity.Finish >= asof)
                .Where(query);
                
            if (orderBy != null)
            {
                queryable = queryable.OrderBy(orderBy);
            }

            return queryable.Select(x => x.Party)
                .Include("Mappings")
                .Include(y => y.Details);
        }
    }
}

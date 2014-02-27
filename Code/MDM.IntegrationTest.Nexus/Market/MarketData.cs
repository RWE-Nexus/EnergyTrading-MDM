namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using Market = EnergyTrading.MDM.Market;

    public partial class MarketData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Market contract)
        {
            var market = ObjectMother.Create<Market>();
            this.repository.Add(market);
            this.repository.Flush();

            contract.Details = new MarketDetails()
                {
                    Calendar = market.Calendar.CreateNexusEntityId(() => market.Calendar.Name),
                    Commodity = market.Commodity.CreateNexusEntityId(() => market.Commodity.Name),
                    Location = market.Location.CreateNexusEntityId(() => market.Location.Name),
                    Name = Guid.NewGuid().ToString(),
                    Currency = "euro",
                    TradeUnits = "Kwh",
                    NominationUnits = "Kwh"
                };
        }

        partial void AddDetailsToEntity(MDM.Market entity, DateTime startDate, DateTime endDate)
        {
            var market = ObjectMother.Create<Market>();
            this.repository.Add(market);
            this.repository.Flush();

            entity.Calendar = market.Calendar;
            entity.Commodity = market.Commodity;
            entity.Location = market.Location;
            entity.Name = Guid.NewGuid().ToString();
            entity.Currency = "euro";
            entity.TradeUnits = "Kwh";
            entity.NominationUnits = "Kwh";
        }

        partial void CreateSearchData(Search search, MDM.Market entity1, MDM.Market entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
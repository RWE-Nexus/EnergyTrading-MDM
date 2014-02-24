namespace EnergyTrading.MDM.Test
{
    using System;
    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    public partial class InstrumentTypeData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.InstrumentType contract)
        {
            contract.Details = new InstrumentTypeDetails() { Name = Guid.NewGuid().ToString() };
        }

        partial void AddDetailsToEntity(MDM.InstrumentType entity, DateTime startDate, DateTime endDate)
        {
            entity.Name = Guid.NewGuid().ToString();
        }

        partial void CreateSearchData(Search search, MDM.InstrumentType entity1, MDM.InstrumentType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
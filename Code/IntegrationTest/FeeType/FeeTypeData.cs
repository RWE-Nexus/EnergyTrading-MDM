namespace EnergyTrading.MDM.Test
{
    using System;
    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Search;

    public partial class FeeTypeData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.FeeType contract)
        {
            contract.Details = new FeeTypeDetails() { Name = Guid.NewGuid().ToString() };
        }

        partial void AddDetailsToEntity(MDM.FeeType entity, DateTime startDate, DateTime endDate)
        {
            entity.Name = Guid.NewGuid().ToString();
        }

        partial void CreateSearchData(Search search, MDM.FeeType entity1, MDM.FeeType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}
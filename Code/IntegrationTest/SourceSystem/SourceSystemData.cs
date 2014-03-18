namespace EnergyTrading.MDM.Test
{
    using System;
	using EnergyTrading.Contracts.Search;
    using EnergyTrading.Search;

    public partial class SourceSystemData
    {
        partial void AddDetailsToContract(EnergyTrading.Mdm.Contracts.SourceSystem contract)
        {
            contract.Details.Name = Guid.NewGuid().ToString();
        }

        partial void AddDetailsToEntity(MDM.SourceSystem entity, DateTime startDate, DateTime endDate)
        {
            entity.Name = Guid.NewGuid().ToString();
        }

        partial void CreateSearchData(Search search, SourceSystem entity1, SourceSystem entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Name", SearchCondition.Equals, entity1.Name)
                .AddCriteria("Name", SearchCondition.Equals, entity2.Name);
        }
    }
}

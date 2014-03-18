namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="PortfolioHierarchy" />
    /// </summary>
    public class PortfolioHierarchyMapper : ContractMapper<PortfolioHierarchy, MDM.PortfolioHierarchy, PortfolioHierarchyDetails, MDM.PortfolioHierarchy, PortfolioHierarchyMapping>
    {
        public PortfolioHierarchyMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override PortfolioHierarchyDetails ContractDetails(PortfolioHierarchy contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(PortfolioHierarchy contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(PortfolioHierarchy contract)
        {
            return contract.Identifiers;
        }
    }
}
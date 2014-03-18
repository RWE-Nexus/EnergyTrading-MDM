namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Hierarchy" />
    /// </summary>
    public class HierarchyMapper : ContractMapper<Hierarchy, MDM.Hierarchy, HierarchyDetails, MDM.Hierarchy, HierarchyMapping>
    {
        public HierarchyMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override HierarchyDetails ContractDetails(Hierarchy contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Hierarchy contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Hierarchy contract)
        {
            return contract.Identifiers;
        }
    }
}
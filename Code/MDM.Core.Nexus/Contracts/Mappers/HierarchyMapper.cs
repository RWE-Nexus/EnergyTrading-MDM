namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Hierarchy contract)
        {
            return contract.Identifiers;
        }
    }
}
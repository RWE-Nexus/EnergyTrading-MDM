namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="SourceSystem" />
    /// </summary>
    public class SourceSystemMapper : ContractMapper<SourceSystem, MDM.SourceSystem, SourceSystemDetails, MDM.SourceSystem, SourceSystemMapping>
    {
        public SourceSystemMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override SourceSystemDetails ContractDetails(SourceSystem contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(SourceSystem contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(SourceSystem contract)
        {
            return contract.Identifiers;
        }
    }
}
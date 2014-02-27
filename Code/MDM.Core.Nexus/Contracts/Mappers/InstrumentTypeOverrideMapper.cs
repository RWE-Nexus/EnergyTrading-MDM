namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="InstrumentTypeOverride" />
    /// </summary>
    public class InstrumentTypeOverrideMapper : ContractMapper<InstrumentTypeOverride, MDM.InstrumentTypeOverride, InstrumentTypeOverrideDetails, MDM.InstrumentTypeOverride, InstrumentTypeOverrideMapping>
    {
        public InstrumentTypeOverrideMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override InstrumentTypeOverrideDetails ContractDetails(InstrumentTypeOverride contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(InstrumentTypeOverride contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(InstrumentTypeOverride contract)
        {
            return contract.Identifiers;
        }
    }
}
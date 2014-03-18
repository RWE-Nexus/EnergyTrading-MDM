namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(InstrumentTypeOverride contract)
        {
            return contract.Identifiers;
        }
    }
}
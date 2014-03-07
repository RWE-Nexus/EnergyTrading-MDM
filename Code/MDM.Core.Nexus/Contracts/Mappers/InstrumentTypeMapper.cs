namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="InstrumentType" />
    /// </summary>
    public class InstrumentTypeMapper : ContractMapper<InstrumentType, MDM.InstrumentType, InstrumentTypeDetails, MDM.InstrumentType, InstrumentTypeMapping>
    {
        public InstrumentTypeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override InstrumentTypeDetails ContractDetails(InstrumentType contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(InstrumentType contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(InstrumentType contract)
        {
            return contract.Identifiers;
        }
    }
}
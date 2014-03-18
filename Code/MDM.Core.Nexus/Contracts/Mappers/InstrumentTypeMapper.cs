namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(InstrumentType contract)
        {
            return contract.Identifiers;
        }
    }
}
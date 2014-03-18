namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="CommodityInstrumentType" />
    /// </summary>
    public class CommodityInstrumentTypeMapper : ContractMapper<CommodityInstrumentType, MDM.CommodityInstrumentType, CommodityInstrumentTypeDetails, MDM.CommodityInstrumentType, CommodityInstrumentTypeMapping>
    {
        public CommodityInstrumentTypeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override CommodityInstrumentTypeDetails ContractDetails(CommodityInstrumentType contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(CommodityInstrumentType contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(CommodityInstrumentType contract)
        {
            return contract.Identifiers;
        }
    }
}
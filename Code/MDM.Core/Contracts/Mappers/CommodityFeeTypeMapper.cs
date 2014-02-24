namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="CommodityFeeType" />
    /// </summary>
    public class CommodityFeeTypeMapper : ContractMapper<CommodityFeeType, MDM.CommodityFeeType, CommodityFeeTypeDetails, MDM.CommodityFeeType, CommodityFeeTypeMapping>
    {
        public CommodityFeeTypeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override CommodityFeeTypeDetails ContractDetails(CommodityFeeType contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(CommodityFeeType contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(CommodityFeeType contract)
        {
            return contract.Identifiers;
        }
    }
}
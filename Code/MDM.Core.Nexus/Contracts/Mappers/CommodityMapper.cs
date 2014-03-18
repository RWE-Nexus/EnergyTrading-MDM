namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Commodity" />
    /// </summary>
    public class CommodityMapper : ContractMapper<Commodity, MDM.Commodity, CommodityDetails, MDM.Commodity, CommodityMapping>
    {
        public CommodityMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override CommodityDetails ContractDetails(Commodity contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Commodity contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Commodity contract)
        {
            return contract.Identifiers;
        }
    }
}
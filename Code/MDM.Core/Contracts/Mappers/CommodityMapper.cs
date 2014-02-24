namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Commodity contract)
        {
            return contract.Identifiers;
        }
    }
}
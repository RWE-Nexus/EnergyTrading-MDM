namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="BrokerCommodity" />
    /// </summary>
    public class BrokerCommodityMapper : ContractMapper<BrokerCommodity, MDM.BrokerCommodity, BrokerCommodityDetails, MDM.BrokerCommodity, BrokerCommodityMapping>
    {
        public BrokerCommodityMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override BrokerCommodityDetails ContractDetails(BrokerCommodity contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(BrokerCommodity contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(BrokerCommodity contract)
        {
            return contract.Identifiers;
        }
    }
}
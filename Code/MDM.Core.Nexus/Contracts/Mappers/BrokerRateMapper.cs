namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="BrokerRate" />
    /// </summary>
    public class BrokerRateMapper : ContractMapper<BrokerRate, MDM.BrokerRate, BrokerRateDetails, MDM.BrokerRateDetails, BrokerRateMapping>
    {
        public BrokerRateMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override BrokerRateDetails ContractDetails(BrokerRate contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(BrokerRate contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(BrokerRate contract)
        {
            return contract.Identifiers;
        }
    }
}
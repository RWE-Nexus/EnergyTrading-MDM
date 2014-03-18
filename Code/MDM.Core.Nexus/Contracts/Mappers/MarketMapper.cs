namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Market" />
    /// </summary>
    public class MarketMapper : ContractMapper<Market, MDM.Market, MarketDetails, MDM.Market, MarketMapping>
    {
        public MarketMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override MarketDetails ContractDetails(Market contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Market contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Market contract)
        {
            return contract.Identifiers;
        }
    }
}
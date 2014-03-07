namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Portfolio" />
    /// </summary>
    public class PortfolioMapper : ContractMapper<Portfolio, MDM.Portfolio, PortfolioDetails, MDM.Portfolio, PortfolioMapping>
    {
        public PortfolioMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override PortfolioDetails ContractDetails(Portfolio contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Portfolio contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(Portfolio contract)
        {
            return contract.Identifiers;
        }
    }
}
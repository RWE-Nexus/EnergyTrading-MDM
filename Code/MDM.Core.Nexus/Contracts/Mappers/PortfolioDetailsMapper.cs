using EnergyTrading.Data;
using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class PortfolioDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.PortfolioDetails, MDM.Portfolio>
    {
        private readonly IRepository repository;

        public PortfolioDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.PortfolioDetails source, MDM.Portfolio destination)
        {
            destination.Name = source.Name;
            destination.PortfolioType = source.PortfolioType;
            destination.BusinessUnit = this.repository.FindEntityByMapping<MDM.PartyRole, PartyRoleMapping>(source.BusinessUnit);
        }
    }
}
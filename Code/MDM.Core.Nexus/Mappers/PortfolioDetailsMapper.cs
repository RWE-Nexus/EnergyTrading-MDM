using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="Portfolio" /> to a <see cref="RWEST.Nexus.MDM.Contracts.PortfolioDetails" />
    /// </summary>
    public class PortfolioDetailsMapper : Mapper<EnergyTrading.MDM.Portfolio, OpenNexus.MDM.Contracts.PortfolioDetails>
    {
        public override void Map(EnergyTrading.MDM.Portfolio source, OpenNexus.MDM.Contracts.PortfolioDetails destination)
        {
            destination.Name = source.Name;
            destination.PortfolioType = source.PortfolioType;
            destination.BusinessUnit = source.BusinessUnit.CreateNexusEntityId(() => source.BusinessUnit.LatestDetails.Name);
        }
    }
}
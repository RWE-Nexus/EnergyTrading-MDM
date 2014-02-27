namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PortfolioMapper : Mapper<EnergyTrading.MDM.Portfolio, RWEST.Nexus.MDM.Contracts.Portfolio>
    {
        public override void Map(EnergyTrading.MDM.Portfolio source, RWEST.Nexus.MDM.Contracts.Portfolio destination)
        {
        }
    }
}

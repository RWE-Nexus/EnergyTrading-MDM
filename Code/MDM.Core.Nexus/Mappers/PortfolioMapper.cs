namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PortfolioMapper : Mapper<EnergyTrading.MDM.Portfolio, OpenNexus.MDM.Contracts.Portfolio>
    {
        public override void Map(EnergyTrading.MDM.Portfolio source, OpenNexus.MDM.Contracts.Portfolio destination)
        {
        }
    }
}

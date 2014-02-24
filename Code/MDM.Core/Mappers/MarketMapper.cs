namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class MarketMapper : Mapper<EnergyTrading.MDM.Market, RWEST.Nexus.MDM.Contracts.Market>
    {
        public override void Map(EnergyTrading.MDM.Market source, RWEST.Nexus.MDM.Contracts.Market destination)
        {
        }
    }
}

namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class MarketMapper : Mapper<EnergyTrading.MDM.Market, OpenNexus.MDM.Contracts.Market>
    {
        public override void Map(EnergyTrading.MDM.Market source, OpenNexus.MDM.Contracts.Market destination)
        {
        }
    }
}

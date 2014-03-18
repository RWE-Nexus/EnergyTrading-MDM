namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class CommodityMapper : Mapper<EnergyTrading.MDM.Commodity, OpenNexus.MDM.Contracts.Commodity>
    {
        public override void Map(EnergyTrading.MDM.Commodity source, OpenNexus.MDM.Contracts.Commodity destination)
        {
        }
    }
}

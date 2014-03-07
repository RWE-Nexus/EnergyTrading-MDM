namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class CommodityMapper : Mapper<EnergyTrading.MDM.Commodity, RWEST.Nexus.MDM.Contracts.Commodity>
    {
        public override void Map(EnergyTrading.MDM.Commodity source, RWEST.Nexus.MDM.Contracts.Commodity destination)
        {
        }
    }
}

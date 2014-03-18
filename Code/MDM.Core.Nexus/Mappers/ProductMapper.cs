namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ProductMapper : Mapper<EnergyTrading.MDM.Product, OpenNexus.MDM.Contracts.Product>
    {
        public override void Map(EnergyTrading.MDM.Product source, OpenNexus.MDM.Contracts.Product destination)
        {
        }
    }
}

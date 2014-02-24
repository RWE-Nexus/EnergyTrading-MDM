namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class ProductMapper : Mapper<EnergyTrading.MDM.Product, RWEST.Nexus.MDM.Contracts.Product>
    {
        public override void Map(EnergyTrading.MDM.Product source, RWEST.Nexus.MDM.Contracts.Product destination)
        {
        }
    }
}

namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using ProductTypeInstance = EnergyTrading.MDM.ProductTypeInstance;

    public class ProductTypeInstanceDetailsMapper : Mapper<EnergyTrading.MDM.ProductTypeInstance, ProductTypeInstanceDetails>
    {
        public override void Map(EnergyTrading.MDM.ProductTypeInstance source, ProductTypeInstanceDetails destination)
        {
            destination.Delivery = source.Delivery.ToContract();
            destination.DeliveryPeriod = source.DeliveryPeriod;
            destination.Name = source.Name;
            destination.ProductType = source.ProductType.CreateNexusEntityId(() => source.ProductType.Name);
            destination.ShortName = source.ShortName;
            destination.Traded = source.Traded.ToContract();
        }
    }
}
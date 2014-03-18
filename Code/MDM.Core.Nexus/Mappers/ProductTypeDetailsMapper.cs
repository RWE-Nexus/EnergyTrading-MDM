namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using ProductType = EnergyTrading.MDM.ProductType;

    public class ProductTypeDetailsMapper : Mapper<EnergyTrading.MDM.ProductType, ProductTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.ProductType source, ProductTypeDetails destination)
        {
            destination.DeliveryPeriod = source.DeliveryPeriod;
            destination.DeliveryRangeType = source.DeliveryRangeType;
            destination.Name = source.Name;
            destination.IsRelative = source.IsRelative;
            destination.Product = source.Product.CreateNexusEntityId(() => source.Product.Name);
            destination.ShortName = source.ShortName;
            destination.Traded = source.Traded.ToContract();
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="ProductTenorType" /> to a <see cref="RWEST.Nexus.MDM.Contracts.ProductTenorTypeDetails" />
    /// </summary>
    public class ProductTenorTypeDetailsMapper : Mapper<EnergyTrading.MDM.ProductTenorType, OpenNexus.MDM.Contracts.ProductTenorTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.ProductTenorType source, OpenNexus.MDM.Contracts.ProductTenorTypeDetails destination)
        {
            destination.Product = source.Product.CreateNexusEntityId(() => source.Product.Name);
            destination.TenorType = source.TenorType.CreateNexusEntityId(() => source.TenorType.Name);
        }
    }
}
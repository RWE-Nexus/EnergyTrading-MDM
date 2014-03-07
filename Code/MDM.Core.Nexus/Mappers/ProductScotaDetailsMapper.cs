namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="ProductScota" /> to a <see cref="RWEST.Nexus.MDM.Contracts.ProductScotaDetails" />
    /// </summary>
    public class ProductScotaDetailsMapper : Mapper<EnergyTrading.MDM.ProductScota, RWEST.Nexus.MDM.Contracts.ProductScotaDetails>
    {
        public override void Map(EnergyTrading.MDM.ProductScota source, RWEST.Nexus.MDM.Contracts.ProductScotaDetails destination)
        {
            destination.Name = source.Name;
            destination.Product = source.Product.CreateNexusEntityId(() => source.Product.Name);
            destination.ScotaDeliveryPoint = source.ScotaDeliveryPoint.CreateNexusEntityId(() => source.ScotaDeliveryPoint.Name);
            destination.ScotaOrigin = source.ScotaOrigin.CreateNexusEntityId(() => source.ScotaOrigin.Name);
            destination.ScotaContract = source.ScotaContract;
            destination.ScotaRss = source.ScotaRss;
            destination.ScotaVersion = source.ScotaVersion;
        }
    }
}
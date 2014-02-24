namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="Tenor" /> to a <see cref="RWEST.Nexus.MDM.Contracts.TenorDetails" />
    /// </summary>
    public class TenorDetailsMapper : Mapper<EnergyTrading.MDM.Tenor, RWEST.Nexus.MDM.Contracts.TenorDetails>
    {
        public override void Map(EnergyTrading.MDM.Tenor source, RWEST.Nexus.MDM.Contracts.TenorDetails destination)
        {
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
            destination.TenorType = source.TenorType.CreateNexusEntityId(() => source.TenorType.Name);
            destination.IsRelative = source.IsRelative;
            destination.DeliveryPeriod = source.DeliveryPeriod;
            destination.DeliveryRangeType = source.DeliveryRangeType;
            destination.Delivery = source.Delivery.ToContract();
            destination.Traded = source.Traded.ToContract();
        }
    }
}
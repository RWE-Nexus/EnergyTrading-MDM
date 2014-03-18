namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    using TenorType = EnergyTrading.MDM.TenorType;

    /// <summary>
    /// Maps a <see cref="MDM.TenorType" /> to a <see cref="RWEST.Nexus.MDM.Contracts.TenorTypeDetails" />
    /// </summary>
    public class TenorTypeDetailsMapper : Mapper<EnergyTrading.MDM.TenorType, OpenNexus.MDM.Contracts.TenorTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.TenorType source, OpenNexus.MDM.Contracts.TenorTypeDetails destination)
        {
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
            //destination.IsRelative = source.IsRelative;
            //destination.DeliveryRangeType = source.DeliveryRangeType;
            //destination.DeliveryPeriod = source.DeliveryPeriod;
            //destination.Traded = source.Traded.ToContract();
        }
    }
}
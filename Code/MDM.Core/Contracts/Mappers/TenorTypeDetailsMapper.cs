namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;
    using DateRange = EnergyTrading.DateRange;

    public class TenorTypeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.TenorTypeDetails, MDM.TenorType>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.TenorTypeDetails source, MDM.TenorType destination)
        {
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
            //destination.IsRelative = source.IsRelative;
            //destination.DeliveryRangeType = source.DeliveryRangeType;
            //destination.DeliveryPeriod = source.DeliveryPeriod;
            //destination.Traded = new DateRange(source.Traded.StartDate, source.Traded.EndDate);
        }
    }
}
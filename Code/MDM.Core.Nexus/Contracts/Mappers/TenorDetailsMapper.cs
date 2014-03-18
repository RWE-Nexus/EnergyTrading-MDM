namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;
    using DateRange = EnergyTrading.DateRange;

    public class TenorDetailsMapper : Mapper<OpenNexus.MDM.Contracts.TenorDetails, MDM.Tenor>
    {
        private readonly IRepository repository;

        public TenorDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.TenorDetails source, MDM.Tenor destination)
        {
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
            destination.TenorType = this.repository.FindEntityByMapping<MDM.TenorType, TenorTypeMapping>(source.TenorType);
            destination.IsRelative = source.IsRelative;
            destination.DeliveryRangeType = source.DeliveryRangeType;
            destination.DeliveryPeriod = source.DeliveryPeriod;
            destination.Delivery = new DateRange(source.Delivery.StartDate, source.Delivery.EndDate);
            destination.Traded = new DateRange(source.Traded.StartDate, source.Traded.EndDate);
        }
    }
}
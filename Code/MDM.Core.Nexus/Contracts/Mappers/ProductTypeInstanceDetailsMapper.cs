namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.MDM.Data;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class ProductTypeInstanceDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ProductTypeInstanceDetails, MDM.ProductTypeInstance>
    {
        private readonly IRepository repository;

        public ProductTypeInstanceDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ProductTypeInstanceDetails source, MDM.ProductTypeInstance destination)
        {
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
            destination.ProductType = repository.FindEntityByMapping<MDM.ProductType, MDM.ProductTypeMapping>(source.ProductType);
            destination.DeliveryPeriod = source.DeliveryPeriod;
            destination.Delivery = source.Delivery == null ? null : new DateRange(source.Delivery.StartDate, source.Delivery.EndDate);

            var tradedStart = source.Traded != null ? source.Traded.StartDate : DateUtility.MinDate;
            var tradedFinish = source.Traded != null ? source.Traded.EndDate : DateUtility.MaxDate;
            destination.Traded = new DateRange(tradedStart, tradedFinish);
        }
    }
}

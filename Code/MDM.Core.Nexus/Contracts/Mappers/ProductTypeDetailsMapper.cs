namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading;

    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    using DateRange = EnergyTrading.DateRange;

    public class ProductTypeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.ProductTypeDetails, MDM.ProductType>
    {
        private readonly IRepository repository;

        public ProductTypeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.ProductTypeDetails source, MDM.ProductType destination)
        {
            destination.Name = source.Name;
            destination.ShortName = source.ShortName;
            destination.DeliveryRangeType = source.DeliveryRangeType;
            destination.DeliveryPeriod = source.DeliveryPeriod;
            destination.IsRelative = source.IsRelative;
            destination.Product = this.repository.FindEntityByMapping<MDM.Product, ProductMapping>(source.Product);

            var tradedStart = source.Traded != null ? source.Traded.StartDate : DateUtility.MinDate;
            var tradedFinish = source.Traded != null ? source.Traded.EndDate : DateUtility.MaxDate;
            destination.Traded = new DateRange(tradedStart, tradedFinish);
        }
    }
}
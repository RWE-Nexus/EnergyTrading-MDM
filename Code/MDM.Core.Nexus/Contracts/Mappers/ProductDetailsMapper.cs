namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class ProductDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ProductDetails, MDM.Product>
    {
        private readonly IRepository repository;

        public ProductDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ProductDetails source, MDM.Product destination)
        {
            destination.Name = source.Name;
            destination.Market = this.repository.FindEntityByMapping<MDM.Market, MarketMapping>(source.Market);
            destination.Exchange = this.repository.FindEntityByMapping<MDM.Exchange, PartyRoleMapping>(source.Exchange);
            destination.CalendarRule = source.CalendarRule;
            destination.LotSize = source.LotSize;
            destination.Shape = this.repository.FindEntityByMapping<MDM.Shape, ShapeMapping>(source.Shape);
            destination.CommodityInstrumentType = this.repository.FindEntityByMapping<MDM.CommodityInstrumentType, CommodityInstrumentTypeMapping>(source.CommodityInstrumentType);
            destination.DefaultCurve = this.repository.FindEntityByMapping<MDM.Curve, CurveMapping>(source.DefaultCurve);
            destination.IncoTerms = source.IncoTerms;
            destination.InstrumentSubType = source.InstrumentSubType;
        }
    }
}

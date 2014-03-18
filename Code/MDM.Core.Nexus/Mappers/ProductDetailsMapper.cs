namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using Product = EnergyTrading.MDM.Product;

    public class ProductDetailsMapper : Mapper<EnergyTrading.MDM.Product, ProductDetails>
    {
        public override void Map(EnergyTrading.MDM.Product source, ProductDetails destination)
        {
            destination.Name = source.Name;
            destination.CalendarRule = source.CalendarRule;
            destination.Market = source.Market.CreateNexusEntityId(() => source.Market.Name);
            destination.Exchange = source.Exchange.CreateNexusEntityId(() => source.Exchange.LatestDetails.Name);
            destination.Shape = source.Shape.CreateNexusEntityId(() => source.Shape.Name);
            destination.CommodityInstrumentType = source.CommodityInstrumentType.CreateNexusEntityId(() =>
                    string.Format(
                        "{0}|{1}|{2}",
                        source.CommodityInstrumentType.Commodity == null ? string.Empty : source.CommodityInstrumentType.Commodity.Name,
                        source.CommodityInstrumentType.InstrumentType == null ? string.Empty : source.CommodityInstrumentType.InstrumentType.Name,
                        source.CommodityInstrumentType.InstrumentDelivery));
            destination.DefaultCurve = source.DefaultCurve.CreateNexusEntityId(() => source.DefaultCurve.Name);
            destination.LotSize = source.LotSize;
            destination.IncoTerms = source.IncoTerms;
            destination.InstrumentSubType = source.InstrumentSubType;
        }
    }
}
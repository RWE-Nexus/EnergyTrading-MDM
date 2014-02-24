namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="InstrumentTypeOverride" /> to a <see cref="RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails" />
    /// </summary>
    public class InstrumentTypeOverrideDetailsMapper : Mapper<EnergyTrading.MDM.InstrumentTypeOverride, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails>
    {
        public override void Map(EnergyTrading.MDM.InstrumentTypeOverride source, RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails destination)
        {
            destination.Name = source.Name;
            destination.ProductType = source.ProductType.CreateNexusEntityId(() => source.ProductType.Name);
            destination.Broker = source.Broker.CreateNexusEntityId(() => source.Broker.LatestDetails.Name);
            
            destination.CommodityInstrumentType =
                source.CommodityInstrumentType.CreateNexusEntityId(
                    () =>
                    string.Format(
                        "{0}|{1}|{2}",
                        source.CommodityInstrumentType.Commodity == null ? string.Empty : source.CommodityInstrumentType.Commodity.Name,
                        source.CommodityInstrumentType.InstrumentType == null ? string.Empty : source.CommodityInstrumentType.InstrumentType.Name,
                        source.CommodityInstrumentType.InstrumentDelivery));

            destination.InstrumentSubType = source.InstrumentSubType;
            destination.ProductTenorType = source.ProductTenorType.CreateNexusEntityId(() =>
                string.Format("{0}|{1}", source.ProductTenorType.Product.Name, source.ProductTenorType.TenorType.Name));
        }
    }
}
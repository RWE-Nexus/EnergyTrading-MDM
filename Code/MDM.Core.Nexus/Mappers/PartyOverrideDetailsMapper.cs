namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="PartyOverride" /> to a <see cref="RWEST.Nexus.MDM.Contracts.PartyOverrideDetails" />
    /// </summary>
    public class PartyOverrideDetailsMapper : Mapper<EnergyTrading.MDM.PartyOverride, RWEST.Nexus.MDM.Contracts.PartyOverrideDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyOverride source, RWEST.Nexus.MDM.Contracts.PartyOverrideDetails destination)
        {

            destination.Broker = source.Broker.CreateNexusEntityId(() => source.Broker.LatestDetails.Name);

            destination.CommodityInstrumentType =
                source.CommodityInstrumentType.CreateNexusEntityId(
                    () =>
                    string.Format(
                        "{0}|{1}|{2}",
                        source.CommodityInstrumentType.Commodity == null ? string.Empty : source.CommodityInstrumentType.Commodity.Name,
                        source.CommodityInstrumentType.InstrumentType == null ? string.Empty : source.CommodityInstrumentType.InstrumentType.Name,
                        source.CommodityInstrumentType.InstrumentDelivery));


            destination.MappingValue = source.MappingValue;

            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
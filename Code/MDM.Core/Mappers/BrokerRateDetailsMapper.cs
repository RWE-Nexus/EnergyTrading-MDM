using System;
using RWEST.Nexus.MDM.Contracts;
using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    /// <summary>
    /// Maps a <see cref="MDM.BrokerRate" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BrokerRateDetails" />
    /// </summary>
    public class BrokerRateDetailsMapper : Mapper<EnergyTrading.MDM.BrokerRateDetails, RWEST.Nexus.MDM.Contracts.BrokerRateDetails>
    {
        public override void Map(EnergyTrading.MDM.BrokerRateDetails source, RWEST.Nexus.MDM.Contracts.BrokerRateDetails destination)
        {
            destination.Broker = source.Broker.CreateNexusEntityId(() => source.Broker.LatestDetails.Name);
            destination.Desk = source.Desk.CreateNexusEntityId(() => source.Desk.LatestDetails.Name);
            destination.CommodityInstrumentType =
                source.CommodityInstrumentType.CreateNexusEntityId(
                    () =>
                    string.Format(
                        "{0}|{1}|{2}",
                        source.CommodityInstrumentType.Commodity == null ? string.Empty : source.CommodityInstrumentType.Commodity.Name,
                        source.CommodityInstrumentType.InstrumentType == null ? string.Empty : source.CommodityInstrumentType.InstrumentType.Name,
                        source.CommodityInstrumentType.InstrumentDelivery));
            destination.Location = source.Location.CreateNexusEntityId(() => source.Location.Name);
            destination.ProductType = source.ProductType.CreateNexusEntityId(() => source.ProductType.Name);
            destination.PartyAction = (PartyAction)Enum.ToObject(typeof(PartyAction), source.PartyAction);
            destination.Rate = source.Rate;
            destination.RateType = source.RateType;
            destination.Currency = source.Currency;
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="SettlementContact" /> to a <see cref="RWEST.Nexus.MDM.Contracts.SettlementContactDetails" />
    /// </summary>
    public class SettlementContactDetailsMapper : Mapper<EnergyTrading.MDM.SettlementContact, OpenNexus.MDM.Contracts.SettlementContactDetails>
    {
        public override void Map(EnergyTrading.MDM.SettlementContact source, OpenNexus.MDM.Contracts.SettlementContactDetails destination)
        {
            destination.Name = source.Name;
            destination.CommodityInstrumentType = source.CommodityInstrumentType.CreateNexusEntityId(() => string.Format(
                        "{0}|{1}|{2}",
                        source.CommodityInstrumentType.Commodity == null ? string.Empty : source.CommodityInstrumentType.Commodity.Name,
                        source.CommodityInstrumentType.InstrumentType == null ? string.Empty : source.CommodityInstrumentType.InstrumentType.Name,
                        source.CommodityInstrumentType.InstrumentDelivery));
            destination.Location = source.Location.CreateNexusEntityId(() => source.Location.Name);
        }
    }
}
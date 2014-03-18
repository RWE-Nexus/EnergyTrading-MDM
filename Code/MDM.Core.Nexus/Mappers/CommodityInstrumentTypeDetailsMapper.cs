namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="CommodityInstrumentType" /> to a <see cref="RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails" />
    /// </summary>
    public class CommodityInstrumentTypeDetailsMapper : Mapper<EnergyTrading.MDM.CommodityInstrumentType, OpenNexus.MDM.Contracts.CommodityInstrumentTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.CommodityInstrumentType source, OpenNexus.MDM.Contracts.CommodityInstrumentTypeDetails destination)
        {
            destination.InstrumentType = source.InstrumentType.CreateNexusEntityId(() => source.InstrumentType.Name);
            destination.Commodity = source.Commodity.CreateNexusEntityId(() => source.Commodity.Name);
            destination.InstrumentDelivery = source.InstrumentDelivery;
        }
    }
}
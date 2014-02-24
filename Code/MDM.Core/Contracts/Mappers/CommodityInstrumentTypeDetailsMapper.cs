namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    /// <summary>
	///
	/// </summary>
    public class CommodityInstrumentTypeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails, MDM.CommodityInstrumentType>
    {
        private readonly IRepository repository;

        public CommodityInstrumentTypeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.CommodityInstrumentTypeDetails source, MDM.CommodityInstrumentType destination)
        {
            destination.Commodity = this.repository.FindEntityByMapping<MDM.Commodity, CommodityMapping>(source.Commodity);
            destination.InstrumentType = this.repository.FindEntityByMapping<MDM.InstrumentType, MDM.InstrumentTypeMapping>(source.InstrumentType);
            destination.InstrumentDelivery = source.InstrumentDelivery;
        }
    }
}
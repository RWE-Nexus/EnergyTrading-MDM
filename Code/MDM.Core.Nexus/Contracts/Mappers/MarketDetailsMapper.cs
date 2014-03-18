namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.MDM.Data;
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;

    public class MarketDetailsMapper : Mapper<OpenNexus.MDM.Contracts.MarketDetails, MDM.Market>
    {
        private readonly IRepository repository;

        public MarketDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.MarketDetails source, MDM.Market destination)
        {
            destination.Name = source.Name;
            destination.Calendar = this.repository.FindEntityByMapping<MDM.Calendar, CalendarMapping>(source.Calendar);
            destination.Commodity = this.repository.FindEntityByMapping<MDM.Commodity, CommodityMapping>(source.Commodity);
            destination.Location = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.Location);
            destination.Currency = source.Currency;
            destination.TradeUnits = source.TradeUnits;
            destination.TradeUnitsRate = source.TradeUnitsRate;
            destination.NominationUnits = source.NominationUnits;
            destination.PriceUnits = source.PriceUnits;
            destination.DeliveryRate = source.DeliveryRate;
            destination.IncoTerms = source.IncoTerms;
        }
    }
}

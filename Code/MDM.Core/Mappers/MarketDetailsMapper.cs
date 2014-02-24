namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using Market = EnergyTrading.MDM.Market;

    public class MarketDetailsMapper : Mapper<EnergyTrading.MDM.Market, MarketDetails>
    {
        public override void Map(EnergyTrading.MDM.Market source, MarketDetails destination)
        {
            destination.Name = source.Name;
            destination.Commodity = source.Commodity.CreateNexusEntityId(() => source.Commodity.Name);
            destination.Location = source.Location.CreateNexusEntityId(() => source.Location.Name);
            destination.Calendar = source.Calendar.CreateNexusEntityId(() => source.Calendar.Name);
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
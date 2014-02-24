namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class MarketChecker : Checker<Market>
    {
        public MarketChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Calendar).Id();
            Compare(x => x.Location).Id();
            Compare(x => x.Commodity).Id();
            Compare(x => x.Currency);
            Compare(x => x.TradeUnits);
            Compare(x => x.TradeUnitsRate);
            Compare(x => x.PriceUnits);
            Compare(x => x.DeliveryRate);
            Compare(x => x.IncoTerms);
            Compare(x => x.NominationUnits);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

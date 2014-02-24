namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class MarketMappingChecker : Checker<MarketMapping>
    {
        public MarketMappingChecker()
        {
            Compare(x => x.Market).Id();
        }
    }
}

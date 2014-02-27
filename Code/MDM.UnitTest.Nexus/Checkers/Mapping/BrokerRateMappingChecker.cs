namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BrokerRateMappingChecker : Checker<BrokerRateMapping>
    {
        public BrokerRateMappingChecker()
        {
            Compare(x => x.BrokerRate).Id();
        }
    }
}

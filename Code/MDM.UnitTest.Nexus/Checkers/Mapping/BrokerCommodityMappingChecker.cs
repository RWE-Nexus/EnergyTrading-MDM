namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class BrokerCommodityMappingChecker : Checker<BrokerCommodityMapping>
    {
        public BrokerCommodityMappingChecker()
        {
            Compare(x => x.BrokerCommodity).Id();
        }
    }
}

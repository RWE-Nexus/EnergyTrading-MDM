namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CommodityMappingChecker : Checker<CommodityMapping>
    {
        public CommodityMappingChecker()
        {
            Compare(x => x.Commodity).Id();
        }
    }
}

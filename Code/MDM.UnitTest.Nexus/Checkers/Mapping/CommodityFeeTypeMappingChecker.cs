namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CommodityFeeTypeMappingChecker : Checker<CommodityFeeTypeMapping>
    {
        public CommodityFeeTypeMappingChecker()
        {
            Compare(x => x.CommodityFeeType).Id();
        }
    }
}

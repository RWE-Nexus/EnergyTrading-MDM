namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class CommodityFeeTypeChecker : Checker<CommodityFeeType>
    {
        public CommodityFeeTypeChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Commodity).Id();
            Compare(x => x.FeeType).Id();
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

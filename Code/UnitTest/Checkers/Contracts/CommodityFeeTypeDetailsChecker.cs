namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class CommodityFeeTypeDetailsChecker : Checker<CommodityFeeTypeDetails>
    {
        public CommodityFeeTypeDetailsChecker()
        {
            Compare(x => x.Commodity);
            Compare(x => x.FeeType);
        }
    }
}

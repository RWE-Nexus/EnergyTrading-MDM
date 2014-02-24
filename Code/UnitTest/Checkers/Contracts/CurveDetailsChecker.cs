namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class CurveDetailsChecker : Checker<CurveDetails>
    {
        public CurveDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.CurveType);
            Compare(x => x.Currency);
            Compare(x => x.Commodity);
            Compare(x => x.CommodityUnit);
            Compare(x => x.Location);
            Compare(x => x.Originator);
            Compare(x => x.DefaultSpread);
        }
    }
}

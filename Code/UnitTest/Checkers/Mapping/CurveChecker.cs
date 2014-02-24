namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

   
    public class CurveChecker : Checker<Curve>
    {
        public CurveChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.Type);
            Compare(x => x.Currency);
            Compare(x => x.Commodity).Id();
            Compare(x => x.CommodityUnit);
            Compare(x => x.Location).Id();
            Compare(x => x.Originator).Id();
            Compare(x => x.DefaultSpread);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

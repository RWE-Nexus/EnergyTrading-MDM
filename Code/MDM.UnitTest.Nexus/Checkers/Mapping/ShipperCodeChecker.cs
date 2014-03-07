namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShipperCodeChecker : Checker<ShipperCode>
    {
        public ShipperCodeChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Party).Id();
            Compare(x => x.Location).Id();
            Compare(x => x.Code);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

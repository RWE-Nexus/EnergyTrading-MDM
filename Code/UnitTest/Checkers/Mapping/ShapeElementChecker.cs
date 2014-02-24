namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShapeElementChecker : Checker<ShapeElement>
    {
        public ShapeElementChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.Period);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

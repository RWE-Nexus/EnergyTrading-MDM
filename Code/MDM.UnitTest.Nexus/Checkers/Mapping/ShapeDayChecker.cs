namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ShapeDayChecker : Checker<ShapeDay>
    {
        public ShapeDayChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.DayType);
            Compare(x => x.Shape);
            Compare(x => x.ShapeElement);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

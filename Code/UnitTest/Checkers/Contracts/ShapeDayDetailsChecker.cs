using EnergyTrading.Test;

namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class ShapeDayDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.ShapeDayDetails>
    {
        public ShapeDayDetailsChecker()
        {
            Compare(x => x.DayType);
            Compare(x => x.Shape);
            Compare(x => x.ShapeElement);
        }
    }
}
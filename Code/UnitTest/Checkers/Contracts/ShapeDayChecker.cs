namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class ShapeDayChecker : Checker<RWEST.Nexus.MDM.Contracts.ShapeDay>
    {
        public ShapeDayChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}

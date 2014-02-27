namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class CurveChecker : Checker<RWEST.Nexus.MDM.Contracts.Curve>
    {
        public CurveChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus);
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}
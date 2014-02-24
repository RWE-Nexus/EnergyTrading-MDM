namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class UnitChecker : Checker<RWEST.Nexus.MDM.Contracts.Unit>
    {
        public UnitChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}

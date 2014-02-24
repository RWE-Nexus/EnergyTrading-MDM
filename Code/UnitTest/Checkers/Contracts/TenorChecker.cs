namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class TenorChecker : Checker<RWEST.Nexus.MDM.Contracts.Tenor>
    {
        public TenorChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}

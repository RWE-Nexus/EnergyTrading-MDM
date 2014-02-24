namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class AgreementChecker : Checker<RWEST.Nexus.MDM.Contracts.Agreement>
    {
        public AgreementChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);      
        }
    }
}

namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class PartyAccountabilityChecker : Checker<RWEST.Nexus.MDM.Contracts.PartyAccountability>
    {
        public PartyAccountabilityChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}
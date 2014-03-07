namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class PartyRoleAccountabilityChecker : Checker<RWEST.Nexus.MDM.Contracts.PartyRoleAccountability>
    {
        public PartyRoleAccountabilityChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus);
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}
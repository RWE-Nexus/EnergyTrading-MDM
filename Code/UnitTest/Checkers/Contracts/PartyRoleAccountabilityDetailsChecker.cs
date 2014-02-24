namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class PartyRoleAccountabilityDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.PartyRoleAccountabilityDetails>
    {
        public PartyRoleAccountabilityDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}

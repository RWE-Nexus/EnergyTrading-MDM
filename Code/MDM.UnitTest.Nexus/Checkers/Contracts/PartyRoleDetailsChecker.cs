namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    using PartyRoleDetails = RWEST.Nexus.MDM.Contracts.PartyRoleDetails;

    public class PartyRoleDetailsChecker : Checker<PartyRoleDetails>
    {
        public PartyRoleDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}

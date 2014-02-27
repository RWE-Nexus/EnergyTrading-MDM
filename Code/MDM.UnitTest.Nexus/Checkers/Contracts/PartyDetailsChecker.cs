namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class PartyDetailsChecker : Checker<PartyDetails>
    {
        public PartyDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.TelephoneNumber);
            Compare(x => x.FaxNumber);
            Compare(x => x.Role);
            Compare(x => x.IsInternal);
        }
    }
}
namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class PartyAccountabilityDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.PartyAccountabilityDetails>
    {
        public PartyAccountabilityDetailsChecker()
        {
            Compare(x => x.Name);		
        }
    }
}

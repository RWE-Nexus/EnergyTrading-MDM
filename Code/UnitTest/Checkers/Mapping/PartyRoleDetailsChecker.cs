namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using EnergyTrading.Test;

    public class PartyRoleDetailsChecker : Checker<PartyRoleDetails>
    {
        public PartyRoleDetailsChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.PartyRole).Id();
            Compare(x => x.Validity);
        }
    }
}
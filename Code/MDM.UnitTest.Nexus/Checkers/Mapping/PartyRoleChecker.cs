namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyRoleChecker : Checker<PartyRole>
    {
        public PartyRoleChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Party).Id();
            Compare(x => x.Details).Count();
            Compare(x => x.Mappings).Count();
        }
    }
}
namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyRoleMappingChecker : Checker<PartyRoleMapping>
    {
        public PartyRoleMappingChecker()
        {
            Compare(x => x.PartyRole).Id();
        }
    }
}
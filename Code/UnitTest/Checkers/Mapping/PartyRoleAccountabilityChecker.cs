namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyRoleAccountabilityChecker : Checker<PartyRoleAccountability>
    {
        public PartyRoleAccountabilityChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.SourcePartyRole);
            Compare(x => x.TargetPartyRole);
            Compare(x => x.Mappings).Count();
        }
    }
}


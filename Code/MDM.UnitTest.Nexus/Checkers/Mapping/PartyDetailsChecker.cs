namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyDetailsChecker : Checker<PartyDetails>
    {
        public PartyDetailsChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Party).Id();
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.Role);
            Compare(x => x.Validity);
            Compare(x => x.IsInternal);
        }
    }
}


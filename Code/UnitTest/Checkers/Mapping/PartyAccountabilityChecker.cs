namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyAccountabilityChecker : Checker<PartyAccountability>
    {
        public PartyAccountabilityChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.SourceParty);
            Compare(x => x.SourcePerson);
            Compare(x => x.TargetParty);
            Compare(x => x.TargetPerson);
            Compare(x => x.Mappings).Count();
        }
    }
}


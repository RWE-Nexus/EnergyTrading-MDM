namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class SettlementContactChecker : Checker<SettlementContact>
    {
        public SettlementContactChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.SourceParty);
            Compare(x => x.TargetParty);
            Compare(x => x.SourcePerson);
            Compare(x => x.TargetPerson);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

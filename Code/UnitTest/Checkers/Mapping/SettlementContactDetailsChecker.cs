namespace RWEST.Nexus.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.Test;

    public class SettlementContactDetailsChecker : Checker<SettlementContactDetails>
    {
        public SettlementContactDetailsChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.Validity);
        }
    }
}


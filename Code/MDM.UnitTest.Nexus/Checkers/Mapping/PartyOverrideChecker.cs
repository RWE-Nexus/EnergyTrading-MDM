namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyOverrideChecker : Checker<PartyOverride>
    {
        public PartyOverrideChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Broker);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.MappingValue);
            Compare(x => x.Party);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

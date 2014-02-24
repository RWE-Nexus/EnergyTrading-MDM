namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class PartyOverrideDetailsChecker : Checker<PartyOverrideDetails>
    {
        public PartyOverrideDetailsChecker()
        {
            Compare(x => x.Broker);
            Compare(x => x.CommodityInstrumentType);
            Compare(x => x.MappingValue);
            Compare(x => x.Party);
        }
    }
}

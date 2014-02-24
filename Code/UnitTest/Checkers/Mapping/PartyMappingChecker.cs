namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyMappingChecker : Checker<PartyMapping>
    {
        public PartyMappingChecker()
        {
            Compare(x => x.Party).Id();
        }
   }
}

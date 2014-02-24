namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PartyOverrideMappingChecker : Checker<PartyOverrideMapping>
    {
        public PartyOverrideMappingChecker()
        {
            Compare(x => x.PartyOverride).Id();
        }
    }
}

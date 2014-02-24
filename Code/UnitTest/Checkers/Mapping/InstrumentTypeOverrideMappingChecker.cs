namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class InstrumentTypeOverrideMappingChecker : Checker<InstrumentTypeOverrideMapping>
    {
        public InstrumentTypeOverrideMappingChecker()
        {
            Compare(x => x.InstrumentTypeOverride).Id();
        }
    }
}

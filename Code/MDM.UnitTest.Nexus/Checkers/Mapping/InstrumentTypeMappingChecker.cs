namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class InstrumentTypeMappingChecker : Checker<InstrumentTypeMapping>
    {
        public InstrumentTypeMappingChecker()
        {
            Compare(x => x.InstrumentType).Id();
        }
    }
}

namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class SourceSystemMappingChecker : Checker<SourceSystemMapping>
    {
        public SourceSystemMappingChecker()
        {
            Compare(x => x.SourceSystem).Id();
        }
    }
}

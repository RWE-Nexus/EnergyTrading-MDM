namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class DimensionMappingChecker : Checker<DimensionMapping>
    {
        public DimensionMappingChecker()
        {
            Compare(x => x.Dimension).Id();
        }
    }
}

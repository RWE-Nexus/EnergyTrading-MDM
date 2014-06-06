namespace EnergyTrading.Mdm.Test.Checkers.Mapping
{
    using EnergyTrading.Mdm;
    using EnergyTrading.Test;

    public class SourceSystemMappingChecker : Checker<SourceSystemMapping>
    {
        public SourceSystemMappingChecker()
        {
            Compare(x => x.SourceSystem).Id();
        }
    }
}

namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class HierarchyMappingChecker : Checker<HierarchyMapping>
    {
        public HierarchyMappingChecker()
        {
            Compare(x => x.Hierarchy).Id();
        }
    }
}

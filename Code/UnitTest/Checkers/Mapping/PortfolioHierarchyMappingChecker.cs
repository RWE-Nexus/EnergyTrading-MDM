namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PortfolioHierarchyMappingChecker : Checker<PortfolioHierarchyMapping>
    {
        public PortfolioHierarchyMappingChecker()
        {
            Compare(x => x.PortfolioHierarchy).Id();
        }
    }
}

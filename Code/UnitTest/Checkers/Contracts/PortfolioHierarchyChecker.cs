namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    public class PortfolioHierarchyChecker : Checker<RWEST.Nexus.MDM.Contracts.PortfolioHierarchy>
    {
        public PortfolioHierarchyChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);		
        }
    }
}

namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class PortfolioHierarchyDetailsChecker : Checker<PortfolioHierarchyDetails>
    {
        public PortfolioHierarchyDetailsChecker()
        {
            this.Compare(x => x.Hierarchy);
            this.Compare(x => x.ChildPortfolio);
            this.Compare(x => x.ParentPortfolio);
        }
    }
}
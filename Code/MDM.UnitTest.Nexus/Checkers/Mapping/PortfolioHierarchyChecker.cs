namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PortfolioHierarchyChecker : Checker<PortfolioHierarchy>
    {
        public PortfolioHierarchyChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Hierarachy);
            Compare(x => x.ChildPortfolio);
            Compare(x => x.ParentPortfolio);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

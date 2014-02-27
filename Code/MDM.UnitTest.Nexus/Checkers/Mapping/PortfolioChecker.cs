namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PortfolioChecker : Checker<Portfolio>
    {
        public PortfolioChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.PortfolioType);
            Compare(x => x.BusinessUnit);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PortfolioMappingChecker : Checker<PortfolioMapping>
    {
        public PortfolioMappingChecker()
        {
            Compare(x => x.Portfolio).Id();
        }
    }
}

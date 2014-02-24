namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class TenorTypeDetailsChecker : Checker<TenorTypeDetails>
    {
        public TenorTypeDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.ShortName);
        }
    }
}

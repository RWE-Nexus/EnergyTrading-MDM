namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class ProductTypeDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.ProductTypeDetails>
    {
        public ProductTypeDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.ShortName);
            Compare(x => x.IsRelative);
            Compare(x => x.DeliveryPeriod);
            Compare(x => x.DeliveryRangeType);
            Compare(x => x.Traded);
        }
    }
}

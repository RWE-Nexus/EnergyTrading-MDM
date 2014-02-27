namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class ProductTenorTypeDetailsChecker : Checker<ProductTenorTypeDetails>
    {
        public ProductTenorTypeDetailsChecker()
        {
            Compare(x => x.Product);
            Compare(x => x.TenorType);
        }
    }
}

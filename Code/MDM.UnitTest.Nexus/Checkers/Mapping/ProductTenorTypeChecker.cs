namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductTenorTypeChecker : Checker<ProductTenorType>
    {
        public ProductTenorTypeChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Product);
            Compare(x => x.TenorType);

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

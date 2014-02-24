namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductTenorTypeMappingChecker : Checker<ProductTenorTypeMapping>
    {
        public ProductTenorTypeMappingChecker()
        {
            Compare(x => x.ProductTenorType).Id();
        }
    }
}

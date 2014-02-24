namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductTypeMappingChecker : Checker<ProductTypeMapping>
    {
        public ProductTypeMappingChecker()
        {
            Compare(x => x.ProductType).Id();
        }
    }
}

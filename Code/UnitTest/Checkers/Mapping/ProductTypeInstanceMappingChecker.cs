namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductTypeInstanceMappingChecker : Checker<ProductTypeInstanceMapping>
    {
        public ProductTypeInstanceMappingChecker()
        {
            Compare(x => x.ProductTypeInstance).Id();
        }
    }
}

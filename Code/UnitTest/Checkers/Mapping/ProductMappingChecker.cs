namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductMappingChecker : Checker<ProductMapping>
    {
        public ProductMappingChecker()
        {
            Compare(x => x.Product).Id();
        }
    }
}

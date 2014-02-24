namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductScotaMappingChecker : Checker<ProductScotaMapping>
    {
        public ProductScotaMappingChecker()
        {
            Compare(x => x.ProductScota).Id();
        }
    }
}

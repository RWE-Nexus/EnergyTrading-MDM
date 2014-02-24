namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductTypeInstanceChecker : Checker<ProductTypeInstance>
    {
        public ProductTypeInstanceChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.ShortName);
            Compare(x => x.ProductType).Id();
            Compare(x => x.DeliveryPeriod);
            Compare(x => x.Traded);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
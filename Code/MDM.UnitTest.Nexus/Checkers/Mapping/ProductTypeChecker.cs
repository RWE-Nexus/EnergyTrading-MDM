namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductTypeChecker : Checker<ProductType>
    {
        public ProductTypeChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Name);
            Compare(x => x.ShortName);
            Compare(x => x.DeliveryRangeType);
            Compare(x => x.DeliveryPeriod);
            Compare(x => x.Product).Id();
            Compare(x => x.Traded);
            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}
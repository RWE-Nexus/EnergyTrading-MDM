namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductCurveChecker : Checker<ProductCurve>
    {
        public ProductCurveChecker()
        {
            Compare(x => x.Id);

            Compare(x => x.Name);
            Compare(x => x.Product);
            Compare(x => x.Curve);
            Compare(x => x.ProductCurveType);
            Compare(x => x.ProjectionMethod);
            

            Compare(x => x.Validity);
            Compare(x => x.Mappings).Count();
        }
    }
}

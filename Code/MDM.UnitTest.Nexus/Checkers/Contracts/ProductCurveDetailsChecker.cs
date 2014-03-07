namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using System;

    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;
    using RWEST.Nexus.MDM.Contracts;

    public class ProductCurveDetailsChecker : Checker<ProductCurveDetails>
    {
        public ProductCurveDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Product);
            Compare(x => x.Curve);
            Compare(x => x.ProductCurveType);
            Compare(x => x.ProjectionMethod);

        }
    }
}

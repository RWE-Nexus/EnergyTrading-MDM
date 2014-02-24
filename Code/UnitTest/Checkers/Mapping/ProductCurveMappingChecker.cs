namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class ProductCurveMappingChecker : Checker<ProductCurveMapping>
    {
        public ProductCurveMappingChecker()
        {
            Compare(x => x.ProductCurve).Id();
        }
    }
}

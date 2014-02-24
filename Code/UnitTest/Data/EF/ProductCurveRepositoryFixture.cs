namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductCurveRepositoryFixture : DbSetRepositoryFixture<ProductCurve>
    {
        protected override ProductCurve Default()
        {
            var entity = ObjectMother.Create<ProductCurve>();

            return entity;
        }
    }
}

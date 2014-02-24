namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductCurveMappingRepositoryFixture : DbSetRepositoryFixture<ProductCurveMapping>
    {
        protected override ProductCurveMapping Default()
        {
            var entity = base.Default();

            entity.ProductCurve = ObjectMother.Create<ProductCurve>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

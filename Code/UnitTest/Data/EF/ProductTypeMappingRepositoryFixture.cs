namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductTypeMappingRepositoryFixture : DbSetRepositoryFixture<ProductTypeMapping>
    {
        protected override ProductTypeMapping Default()
        {
            var entity = base.Default();
            entity.ProductType = ObjectMother.Create<ProductType>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

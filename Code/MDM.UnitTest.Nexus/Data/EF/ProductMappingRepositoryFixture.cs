namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductMappingRepositoryFixture : DbSetRepositoryFixture<ProductMapping>
    {
        protected override ProductMapping Default()
        {
            var entity = base.Default();
            entity.Product = ObjectMother.Create<Product>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductTypeInstanceMappingRepositoryFixture : DbSetRepositoryFixture<ProductTypeInstanceMapping>
    {
        protected override ProductTypeInstanceMapping Default()
        {
            var entity = base.Default();
            entity.ProductTypeInstance = ObjectMother.Create<ProductTypeInstance>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

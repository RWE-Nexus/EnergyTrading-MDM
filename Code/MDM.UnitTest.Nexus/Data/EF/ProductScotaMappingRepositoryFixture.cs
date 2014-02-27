namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductScotaMappingRepositoryFixture : DbSetRepositoryFixture<ProductScotaMapping>
    {
        protected override ProductScotaMapping Default()
        {
            var entity = base.Default();
            entity.ProductScota = ObjectMother.Create<ProductScota>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

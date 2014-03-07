namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class TenorTypeMappingRepositoryFixture : DbSetRepositoryFixture<TenorTypeMapping>
    {
        protected override TenorTypeMapping Default()
        {
            var entity = base.Default();
            entity.TenorType = ObjectMother.Create<TenorType>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

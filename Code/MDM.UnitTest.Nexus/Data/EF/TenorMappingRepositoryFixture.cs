namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class TenorMappingRepositoryFixture : DbSetRepositoryFixture<TenorMapping>
    {
        protected override TenorMapping Default()
        {
            var entity = base.Default();
            entity.Tenor = ObjectMother.Create<Tenor>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

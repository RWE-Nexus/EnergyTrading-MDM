namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class UnitMappingRepositoryFixture : DbSetRepositoryFixture<UnitMapping>
    {
        protected override UnitMapping Default()
        {
            var entity = base.Default();
            entity.Unit = ObjectMother.Create<Unit>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

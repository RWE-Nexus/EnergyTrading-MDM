namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class HierarchyMappingRepositoryFixture : DbSetRepositoryFixture<HierarchyMapping>
    {
        protected override HierarchyMapping Default()
        {
            var entity = base.Default();
            entity.Hierarchy = ObjectMother.Create<Hierarchy>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

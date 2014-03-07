namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ShapeDayMappingRepositoryFixture : DbSetRepositoryFixture<ShapeDayMapping>
    {
        protected override ShapeDayMapping Default()
        {
            var entity = base.Default();
            entity.ShapeDay = ObjectMother.Create<ShapeDay>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

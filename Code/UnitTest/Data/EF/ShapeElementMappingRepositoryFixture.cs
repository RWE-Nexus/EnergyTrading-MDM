namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ShapeElementMappingRepositoryFixture : DbSetRepositoryFixture<ShapeElementMapping>
    {
        protected override ShapeElementMapping Default()
        {
            var entity = base.Default();
            entity.ShapeElement = ObjectMother.Create<ShapeElement>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

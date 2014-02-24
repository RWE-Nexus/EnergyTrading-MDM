namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class DimensionMappingRepositoryFixture : DbSetRepositoryFixture<DimensionMapping>
    {
        protected override DimensionMapping Default()
        {
            var entity = base.Default();
            entity.Dimension = ObjectMother.Create<Dimension>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

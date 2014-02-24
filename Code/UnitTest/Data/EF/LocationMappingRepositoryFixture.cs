namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class LocationMappingRepositoryFixture : DbSetRepositoryFixture<LocationMapping>
    {
        protected override LocationMapping Default()
        {
            var entity = base.Default();
            entity.Location = ObjectMother.Create<Location>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

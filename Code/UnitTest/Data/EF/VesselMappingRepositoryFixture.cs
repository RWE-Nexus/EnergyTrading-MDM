namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class VesselMappingRepositoryFixture : DbSetRepositoryFixture<VesselMapping>
    {
        protected override VesselMapping Default()
        {
            var entity = base.Default();
            entity.Vessel = ObjectMother.Create<Vessel>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

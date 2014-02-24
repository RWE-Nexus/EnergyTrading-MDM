namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class LocationRoleMappingRepositoryFixture : DbSetRepositoryFixture<LocationRoleMapping>
    {
        protected override LocationRoleMapping Default()
        {
            var entity = base.Default();
            entity.LocationRole = ObjectMother.Create<LocationRole>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

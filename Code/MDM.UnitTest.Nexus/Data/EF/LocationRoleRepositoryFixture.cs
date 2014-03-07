namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class LocationRoleRepositoryFixture : DbSetRepositoryFixture<LocationRole>
    {
        protected override LocationRole Default()
        {
            var entity = ObjectMother.Create<LocationRole>();

            return entity;
        }
    }
}

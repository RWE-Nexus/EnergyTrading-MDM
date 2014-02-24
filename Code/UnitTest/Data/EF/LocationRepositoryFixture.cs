namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class LocationRepositoryFixture : DbSetRepositoryFixture<Location>
    {
        protected override Location Default()
        {
            var entity = ObjectMother.Create<Location>();

            return entity;
        }
    }
}

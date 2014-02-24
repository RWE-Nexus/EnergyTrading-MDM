namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class VesselRepositoryFixture : DbSetRepositoryFixture<Vessel>
    {
        protected override Vessel Default()
        {
            var entity = ObjectMother.Create<Vessel>();

            return entity;
        }
    }
}

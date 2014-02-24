namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ShipperCodeRepositoryFixture : DbSetRepositoryFixture<ShipperCode>
    {
        protected override ShipperCode Default()
        {
            var entity = ObjectMother.Create<ShipperCode>();

            return entity;
        }
    }
}

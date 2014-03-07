namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BrokerCommodityRepositoryFixture : DbSetRepositoryFixture<BrokerCommodity>
    {
        protected override BrokerCommodity Default()
        {
            var entity = ObjectMother.Create<BrokerCommodity>();

            return entity;
        }
    }
}

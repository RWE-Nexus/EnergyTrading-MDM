namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BrokerRateRepositoryFixture : DbSetRepositoryFixture<BrokerRate>
    {
        protected override BrokerRate Default()
        {
            var entity = ObjectMother.Create<BrokerRate>();

            return entity;
        }
    }
}

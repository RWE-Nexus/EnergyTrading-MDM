namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class BrokerRepositoryFixture : DbSetRepositoryFixture<Broker>
    {
        protected override Broker Default()
        {
            var entity = ObjectMother.Create<Broker>();

            return entity;
        }
    }
}

namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class MarketRepositoryFixture : DbSetRepositoryFixture<Market>
    {
        protected override Market Default()
        {
            var entity = ObjectMother.Create<Market>();

            return entity;
        }
    }
}

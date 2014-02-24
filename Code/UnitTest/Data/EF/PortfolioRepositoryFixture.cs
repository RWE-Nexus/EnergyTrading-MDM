namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PortfolioRepositoryFixture : DbSetRepositoryFixture<Portfolio>
    {
        protected override Portfolio Default()
        {
            var entity = ObjectMother.Create<Portfolio>();

            return entity;
        }
    }
}

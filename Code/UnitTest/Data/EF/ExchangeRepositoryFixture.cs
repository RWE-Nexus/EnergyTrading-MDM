namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ExchangeRepositoryFixture : DbSetRepositoryFixture<Exchange>
    {
        protected override Exchange Default()
        {
            var entity = ObjectMother.Create<Exchange>();

            return entity;
        }
    }
}

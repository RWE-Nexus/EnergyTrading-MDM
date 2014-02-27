namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class SettlementContactRepositoryFixture : DbSetRepositoryFixture<SettlementContact>
    {
        protected override SettlementContact Default()
        {
            var entity = ObjectMother.Create<SettlementContact>();

            return entity;
        }
    }
}

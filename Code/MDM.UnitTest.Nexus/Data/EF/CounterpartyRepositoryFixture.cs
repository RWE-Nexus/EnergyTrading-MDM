namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CounterpartyRepositoryFixture : DbSetRepositoryFixture<Counterparty>
    {
        protected override Counterparty Default()
        {
            var entity = ObjectMother.Create<Counterparty>();

            return entity;
        }
    }
}

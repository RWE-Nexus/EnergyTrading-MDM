namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class TenorRepositoryFixture : DbSetRepositoryFixture<Tenor>
    {
        protected override Tenor Default()
        {
            var entity = ObjectMother.Create<Tenor>();

            return entity;
        }
    }
}

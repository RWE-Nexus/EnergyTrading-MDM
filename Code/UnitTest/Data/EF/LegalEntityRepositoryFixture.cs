namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class LegalEntityRepositoryFixture : DbSetRepositoryFixture<LegalEntity>
    {
        protected override LegalEntity Default()
        {
            var entity = ObjectMother.Create<LegalEntity>();

            return entity;
        }
    }
}

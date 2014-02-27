namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductScotaRepositoryFixture : DbSetRepositoryFixture<ProductScota>
    {
        protected override ProductScota Default()
        {
            var entity = ObjectMother.Create<ProductScota>();

            return entity;
        }
    }
}

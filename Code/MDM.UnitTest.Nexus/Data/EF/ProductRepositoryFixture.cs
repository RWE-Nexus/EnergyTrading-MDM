namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductRepositoryFixture : DbSetRepositoryFixture<Product>
    {
        protected override Product Default()
        {
            var entity = ObjectMother.Create<Product>();

            return entity;
        }
    }
}

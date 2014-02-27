namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductTypeRepositoryFixture : DbSetRepositoryFixture<ProductType>
    {
        protected override ProductType Default()
        {
            var entity = ObjectMother.Create<ProductType>();

            return entity;
        }
    }
}

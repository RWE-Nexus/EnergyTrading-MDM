namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductTenorTypeRepositoryFixture : DbSetRepositoryFixture<ProductTenorType>
    {
        protected override ProductTenorType Default()
        {
            var entity = ObjectMother.Create<ProductTenorType>();

            return entity;
        }
    }
}

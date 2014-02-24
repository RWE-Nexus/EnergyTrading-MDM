namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ProductTypeInstanceRepositoryFixture : DbSetRepositoryFixture<ProductTypeInstance>
    {
        protected override ProductTypeInstance Default()
        {
            var entity = ObjectMother.Create<ProductTypeInstance>();

            return entity;
        }
    }
}

namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class ProductTenorTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var source = ObjectMother.Create<ProductTenorType>();
            var mapper = new MDM.Mappers.ProductTenorTypeDetailsMapper();

            var result = mapper.Map(source);

            Assert.AreEqual(source.Product.Name, result.Product.Name);
            Assert.AreEqual(source.TenorType.Name, result.TenorType.Name);
        }
    }
}

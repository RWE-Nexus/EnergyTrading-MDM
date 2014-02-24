namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProductTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.ProductType()
                {
                    Product = new Product() { Id = 1 }
                };

            var mapper = new MDM.Mappers.ProductTypeDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
        }
    }
}

	
namespace EnergyTrading.MDM.Test.Mappers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;

    [TestClass]
    public class ProductTypeInstanceDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.ProductTypeInstance()
                {
                    ProductType = new ProductType() { Id = 1 },
                    Delivery = new DateRange(
                        DateTime.Today, DateTime.Today.AddDays(1))
                }; 

            var mapper = new MDM.Mappers.ProductTypeInstanceDetailsMapper();

            // Act
            var result = mapper.Map(source);

			// Assert
			Assert.IsNotNull(result);
            Assert.AreEqual("1", result.ProductType.Identifier.Identifier);
            Assert.AreEqual(source.Delivery.Start, result.Delivery.StartDate);
            Assert.AreEqual(source.Delivery.Finish, result.Delivery.EndDate);
        }
    }
}

	
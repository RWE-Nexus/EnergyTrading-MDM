namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class ProductScotaDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<ProductScota>();

            var mapper = new MDM.Mappers.ProductScotaDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Product.Id.ToString(), result.Product.Identifier.Identifier);
            Assert.AreEqual(source.ScotaDeliveryPoint.Id.ToString(), result.ScotaDeliveryPoint.Identifier.Identifier);
            Assert.AreEqual(source.ScotaOrigin.Id.ToString(), result.ScotaOrigin.Identifier.Identifier);
            Assert.AreEqual(source.ScotaContract, result.ScotaContract);
            Assert.AreEqual(source.ScotaRss, result.ScotaRss);
            Assert.AreEqual(source.ScotaVersion, result.ScotaVersion);
        }
    }
}


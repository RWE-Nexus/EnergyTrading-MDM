namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    using Location = EnergyTrading.MDM.Location;
    using Product = EnergyTrading.MDM.Product;

    [TestClass]
    public class ProductScotaDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var product = new Product();
            var scotaDeliveryPoint = new Location();
            var scotaOrigin = new Location();

            mockRepository.Setup(x => x.FindOne<Product>(2)).Returns(product);
            mockRepository.Setup(x => x.FindOne<Location>(4)).Returns(scotaDeliveryPoint);
            mockRepository.Setup(x => x.FindOne<Location>(6)).Returns(scotaOrigin);

            var source = new RWEST.Nexus.MDM.Contracts.ProductScotaDetails
                {
                    Name = "testProductScota",
                    Product = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "2" } },
                    ScotaDeliveryPoint = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "4" } },
                    ScotaOrigin = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "6" } },
                    ScotaContract = "ThisIsStandard",
                    ScotaRss = "RB1",
                    ScotaVersion = "7E"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ProductScotaDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(product, result.Product);
            Assert.AreEqual(scotaDeliveryPoint, result.ScotaDeliveryPoint);
            Assert.AreEqual(scotaOrigin, result.ScotaOrigin);
            Assert.AreEqual(source.ScotaContract, result.ScotaContract);
            Assert.AreEqual(source.ScotaRss, result.ScotaRss);
            Assert.AreEqual(source.ScotaVersion, result.ScotaVersion);
        }
    }
}

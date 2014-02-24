namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using EnergyTrading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data;

    [TestClass]
    public class ProductTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var fakeProduct = new MDM.Product();
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repository => repository.FindOne<MDM.Product>(1)).Returns(fakeProduct);

            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.ProductTypeDetails
                { 
                    Name = "Test DA",
                    ShortName = "DA", 
                    Product = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } },
                    DeliveryPeriod = "DA",
                    IsRelative = true,
                    DeliveryRangeType = "Test",
                    Traded = new RWEST.Nexus.MDM.Contracts.DateRange { StartDate = SystemTime.UtcNow().AddDays(-1), EndDate = SystemTime.UtcNow() },
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ProductTypeDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeProduct, result.Product, "Product differs");
            Assert.AreEqual(source.Name, result.Name, "Name differs");
            Assert.AreEqual(source.ShortName, result.ShortName, "ShortName differs");
            Assert.AreEqual(source.DeliveryPeriod, result.DeliveryPeriod, "DeliveryPeriod differs");
            Assert.AreEqual(source.DeliveryRangeType, result.DeliveryRangeType, "DeliveryRangeType differs");
            Assert.AreEqual(source.IsRelative, result.IsRelative, "IsRelative differs");
            Assert.AreEqual(DateUtility.Round(source.Traded.StartDate.Value), result.Traded.Start, "Traded.Start differs");
            Assert.AreEqual(DateUtility.Round(source.Traded.EndDate.Value), result.Traded.Finish, "Traded.End differs");

        }
    }
}
		
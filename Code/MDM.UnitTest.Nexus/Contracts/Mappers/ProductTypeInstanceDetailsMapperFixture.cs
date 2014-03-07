namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading;
    using EnergyTrading.Data;


    [TestClass]
    public class ProductTypeInstanceDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var fakeProductType = new MDM.ProductType(); 
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repository => repository.FindOne<MDM.ProductType>(1)).Returns(fakeProductType);

            var source = new RWEST.Nexus.MDM.Contracts.ProductTypeInstanceDetails
            {
                Name = "Test",
                ShortName = "ShortName",
                DeliveryPeriod = "xxx",
                ProductType = new EntityId() { Identifier = new NexusId() { Identifier = "1", IsNexusId = true }},
                Delivery = new RWEST.Nexus.MDM.Contracts.DateRange{ StartDate = SystemTime.UtcNow(), EndDate = SystemTime.UtcNow().AddDays(1)},
                Traded = new RWEST.Nexus.MDM.Contracts.DateRange { StartDate = SystemTime.UtcNow().AddDays(-1), EndDate = SystemTime.UtcNow()},
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ProductTypeInstanceDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeProductType, result.ProductType, "ProductType differs");
            Assert.AreEqual(source.Name, result.Name, "Name differs");
            Assert.AreEqual(source.ShortName, result.ShortName, "ShortName differs");
            Assert.AreEqual(source.DeliveryPeriod, result.DeliveryPeriod, "DeliveryPeriod differs");
            Assert.AreEqual(DateUtility.Round(source.Traded.StartDate.Value), result.Traded.Start, "Traded.Start differs");
            Assert.AreEqual(DateUtility.Round(source.Traded.EndDate.Value), result.Traded.Finish, "Traded.End differs");
            Assert.AreEqual(DateUtility.Round(source.Delivery.StartDate.Value), result.Delivery.Start, "Delivery.Start differs");
            Assert.AreEqual(DateUtility.Round(source.Delivery.EndDate.Value), result.Delivery.Finish, "Delivery.End differs");
        }
    }
}
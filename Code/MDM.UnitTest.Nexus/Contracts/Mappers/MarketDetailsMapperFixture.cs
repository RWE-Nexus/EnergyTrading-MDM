namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data;

    [TestClass]
    public class MarketDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeCalendar = new MDM.Calendar();
            var fakeCommodity = new MDM.Commodity();
            var fakeLocation = new MDM.Location();

            mockRepository.Setup(repository => repository.FindOne<MDM.Location>(1)).Returns(fakeLocation);
            mockRepository.Setup(repository => repository.FindOne<MDM.Commodity>(1)).Returns(fakeCommodity);
            mockRepository.Setup(repository => repository.FindOne<MDM.Calendar>(1)).Returns(fakeCalendar);

            var source = new RWEST.Nexus.MDM.Contracts.MarketDetails
            {
                Name = "Test Market",
                Location = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                Commodity = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                Calendar = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                Currency = "EUR",
                TradeUnits = "MW",
                NominationUnits = "KW",
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.MarketDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(fakeCommodity, result.Commodity);
            Assert.AreEqual(fakeLocation, result.Location);
            Assert.AreEqual(fakeCalendar, result.Calendar);
            Assert.AreEqual(source.Currency, result.Currency);
            Assert.AreEqual(source.TradeUnits, result.TradeUnits);
            Assert.AreEqual(source.NominationUnits, result.NominationUnits);
        }
    }
}
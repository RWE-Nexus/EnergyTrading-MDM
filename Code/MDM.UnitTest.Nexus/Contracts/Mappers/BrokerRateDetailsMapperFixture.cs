namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    using Broker = EnergyTrading.MDM.Broker;
    using CommodityInstrumentType = EnergyTrading.MDM.CommodityInstrumentType;
    using Location = EnergyTrading.MDM.Location;
    using PartyRole = EnergyTrading.MDM.PartyRole;
    using ProductType = EnergyTrading.MDM.ProductType;

    [TestClass]
    public class BrokerRateDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeBroker = new Broker();
            var fakeDesk = new PartyRole();
            var fakeCommodityInstumentType = new CommodityInstrumentType();
            var fakeLocation = new Location();
            var fakeProductType = new ProductType();

            mockRepository.Setup(r => r.FindOne<MDM.Broker>(1)).Returns(fakeBroker);
            mockRepository.Setup(r => r.FindOne<MDM.PartyRole>(2)).Returns(fakeDesk);
            mockRepository.Setup(r => r.FindOne<MDM.CommodityInstrumentType>(3)).Returns(fakeCommodityInstumentType);
            mockRepository.Setup(r => r.FindOne<MDM.Location>(4)).Returns(fakeLocation);
            mockRepository.Setup(r => r.FindOne<MDM.ProductType>(5)).Returns(fakeProductType);

            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.BrokerRateDetails
                {
                    Broker = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } },
                    Desk = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "2" } },
                    CommodityInstrumentType = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "3" } },
                    Location = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "4" } },
                    ProductType = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "5" } },
                    PartyAction = PartyAction.Initiator,
                    Rate = 3.4m
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.BrokerRateDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeBroker, result.Broker);
            Assert.AreEqual(fakeDesk, result.Desk);
            Assert.AreEqual(fakeCommodityInstumentType, result.CommodityInstrumentType);
            Assert.AreEqual(fakeLocation, result.Location);
            Assert.AreEqual(fakeProductType, result.ProductType);
            Assert.AreEqual((int)PartyAction.Initiator, result.PartyAction);
            Assert.AreEqual(source.Rate, result.Rate);
        }
    }
}

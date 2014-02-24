using Moq;
using EnergyTrading.Data;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BrokerCommodityDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeBroker = new Broker();
            var fakeCommodity = new Commodity();

            mockRepository.Setup(r => r.FindOne<MDM.Broker>(1)).Returns(fakeBroker);
            mockRepository.Setup(r => r.FindOne<MDM.Commodity>(2)).Returns(fakeCommodity);
            
            var source = new RWEST.Nexus.MDM.Contracts.BrokerCommodityDetails
                {
					Name = "ICAP Gas",
                    Broker = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } },
                    Commodity = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "2" } },
                    
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.BrokerCommodityDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(fakeBroker, result.Broker);
            Assert.AreEqual(fakeCommodity, result.Commodity);
        }
    }
}
		
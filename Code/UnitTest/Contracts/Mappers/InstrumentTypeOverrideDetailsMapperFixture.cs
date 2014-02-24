using Moq;
using EnergyTrading.Data;
using RWEST.Nexus.MDM.Contracts;

namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InstrumentTypeOverrideDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeProductType = new MDM.ProductType();
            var fakeBroker = new MDM.Broker();
            var fakeCommodityInstrumentType = new MDM.CommodityInstrumentType();
            var fakeProductTenorType = new ProductTenorType();


            mockRepository.Setup(repository => repository.FindOne<MDM.ProductType>(1)).Returns(fakeProductType);
            mockRepository.Setup(repository => repository.FindOne<MDM.Broker>(2)).Returns(fakeBroker);
            mockRepository.Setup(repository => repository.FindOne<MDM.CommodityInstrumentType>(3)).Returns(fakeCommodityInstrumentType);
            mockRepository.Setup(repository => repository.FindOne<MDM.ProductTenorType>(4)).Returns(fakeProductTenorType);

            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.InstrumentTypeOverrideDetails
            {
                Name = "testName",
                ProductType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                Broker = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "2" } },
                CommodityInstrumentType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "3" } },
                InstrumentSubType = "Financial",
                ProductTenorType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "4" } },
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.InstrumentTypeOverrideDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(fakeProductType, result.ProductType);
            Assert.AreEqual(fakeBroker, result.Broker);
            Assert.AreEqual(fakeCommodityInstrumentType, result.CommodityInstrumentType);
            Assert.AreEqual(source.InstrumentSubType, result.InstrumentSubType);
            Assert.AreEqual(fakeProductTenorType, result.ProductTenorType);
           
        }
    }
}
		
namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data;

    using Market = EnergyTrading.MDM.Market;

    [TestClass]
    public class ProductDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeMarket = new MDM.Market();
            var fakeExchange = new MDM.Exchange();
            var fakeShape = new MDM.Shape();
            var fakeCommodityInstrumentType = new MDM.CommodityInstrumentType();
            var fakeCurve = new MDM.Curve();

            mockRepository.Setup(repository => repository.FindOne<MDM.Market>(1)).Returns(fakeMarket);
            mockRepository.Setup(repository => repository.FindOne<MDM.Shape>(2)).Returns(fakeShape);
            mockRepository.Setup(repository => repository.FindOne<MDM.CommodityInstrumentType>(3)).Returns(fakeCommodityInstrumentType);
            mockRepository.Setup(repository => repository.FindOne<MDM.Curve>(4)).Returns(fakeCurve);
            mockRepository.Setup(repository => repository.FindOne<MDM.Exchange>(5)).Returns(fakeExchange);

            var source = new RWEST.Nexus.MDM.Contracts.ProductDetails
            {
                Name = "test product details", 
                Market = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } },
                Exchange = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "5" } },
                Shape = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "2" } },
                CommodityInstrumentType = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "3" } },
                DefaultCurve = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "4" } },
                LotSize = 100,
                CalendarRule = "test rule",
                IncoTerms = "test incoterms",
                InstrumentSubType = "floating"
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ProductDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(fakeMarket, result.Market);
            Assert.AreEqual(fakeExchange, result.Exchange);
            Assert.AreEqual(fakeCommodityInstrumentType, result.CommodityInstrumentType);
            Assert.AreEqual(fakeShape, result.Shape);
            Assert.AreEqual(fakeCurve, result.DefaultCurve);
            Assert.AreEqual(source.LotSize, result.LotSize);
            Assert.AreEqual(source.CalendarRule, result.CalendarRule);
            Assert.AreEqual(source.IncoTerms, result.IncoTerms); 
            Assert.AreEqual(source.InstrumentSubType, result.InstrumentSubType); 
        }
    }
}
		
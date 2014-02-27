namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProductDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.Product() 
            { 
                CalendarRule = "rule",
                Market = new Market() { Id = 1 },
                Shape = new Shape() { Id = 2 },
                Exchange = Exchange(3),
                CommodityInstrumentType = new CommodityInstrumentType() { Id = 4 },
                DefaultCurve = new Curve() { Id = 5 },
                Name = "product name",
                LotSize = 5,
                InstrumentSubType = "fixed"
                
                };


            var mapper = new MDM.Mappers.ProductDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("1", result.Market.Identifier.Identifier);
            Assert.AreEqual("2", result.Shape.Identifier.Identifier);
            Assert.AreEqual("3", result.Exchange.Identifier.Identifier);
            Assert.AreEqual("4", result.CommodityInstrumentType.Identifier.Identifier);
            Assert.AreEqual("5", result.DefaultCurve.Identifier.Identifier);
            Assert.AreEqual("rule", result.CalendarRule);
            Assert.AreEqual("product name", result.Name);
            Assert.AreEqual("fixed", result.InstrumentSubType);
            Assert.AreEqual(5, result.LotSize);
        }

        private static Exchange Exchange(int id)
        {
            var exchange = ObjectMother.Create<Exchange>();
            exchange.Id = id;
            return exchange;
        }
    }
}

	
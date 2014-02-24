namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MarketDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.Market()
            {
                Name = "market name",
                Calendar = new Calendar { Id = 1 },
                Commodity = new Commodity { Id = 1},
                Location = new Location { Id = 1 },
                Currency = "GBP",
                TradeUnits = "MW",
                NominationUnits = "KW",
                PriceUnits = "Pence",
                DeliveryRate = "Daily",
                IncoTerms = "Terms"
            };

            var mapper = new MDM.Mappers.MarketDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Calendar.Id.ToString(), result.Calendar.Identifier.Identifier);
            Assert.AreEqual(source.Location.Id.ToString(), result.Location.Identifier.Identifier);
            Assert.AreEqual(source.Commodity.Id.ToString(), result.Commodity.Identifier.Identifier);
            Assert.AreEqual(source.Currency, result.Currency);
            Assert.AreEqual(source.TradeUnits, result.TradeUnits);
            Assert.AreEqual(source.NominationUnits, result.NominationUnits);
            Assert.AreEqual(result.PriceUnits, source.PriceUnits);
            Assert.AreEqual(result.DeliveryRate, source.DeliveryRate);
            Assert.AreEqual(result.IncoTerms, source.IncoTerms);
        }
    }
}
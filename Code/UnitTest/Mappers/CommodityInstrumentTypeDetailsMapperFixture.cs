namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class CommodityInstrumentTypeDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = new CommodityInstrumentType()
	            {
                    Commodity = new Commodity() { Id = 1 },
                    InstrumentType = new InstrumentType() { Id = 2 },
                    InstrumentDelivery = "physical"
	            };

            var mapper = new MDM.Mappers.CommodityInstrumentTypeDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Commodity.Id.ToString(), result.Commodity.Identifier.Identifier);
            Assert.AreEqual(source.InstrumentType.Id.ToString(), result.InstrumentType.Identifier.Identifier);
            Assert.AreEqual(source.InstrumentDelivery, result.InstrumentDelivery);
		}
    }
}
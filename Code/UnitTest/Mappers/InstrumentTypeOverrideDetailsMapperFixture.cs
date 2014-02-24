namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class InstrumentTypeOverrideDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<InstrumentTypeOverride>();

            var mapper = new MDM.Mappers.InstrumentTypeOverrideDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert

            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.ProductType.Id.ToString(), result.ProductType.Identifier.Identifier);
            Assert.AreEqual(source.Broker.Id.ToString(), result.Broker.Identifier.Identifier);
            Assert.AreEqual(source.CommodityInstrumentType.Id.ToString(), result.CommodityInstrumentType.Identifier.Identifier);
            Assert.AreEqual(source.InstrumentSubType, result.InstrumentSubType);
            Assert.AreEqual(source.ProductTenorType.Id.ToString(), result.ProductTenorType.Identifier.Identifier);
		}
    }
}

	
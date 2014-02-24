namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class PartyOverrideDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<PartyOverride>();

            var mapper = new MDM.Mappers.PartyOverrideDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Broker.Id.ToString(), result.Broker.Identifier.Identifier);
            Assert.AreEqual(source.CommodityInstrumentType.Id.ToString(), result.CommodityInstrumentType.Identifier.Identifier);
            Assert.AreEqual(source.MappingValue, result.MappingValue);
            Assert.AreEqual(source.Party.Id.ToString(), result.Party.Identifier.Identifier);
        }
    }
}

	
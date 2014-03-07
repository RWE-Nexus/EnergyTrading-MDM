namespace RWEST.Nexus.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using RWEST.Nexus.MDM.Test;

    [TestClass]
    public class CurveDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new MDM.Curve()
            {
                Name = "curve name",
                Type = "Forward",
                Currency = "GBP",
                Commodity = new Commodity() { Id = 1 },
                CommodityUnit = "ton",
                Location = new Location() { Id = 1 },
                DefaultSpread = .005m
            };

            var mapper = new MDM.Mappers.CurveDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
			// TODO_UnitTestGeneration_Curve
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Type, result.CurveType);
            Assert.AreEqual(source.Currency, result.Currency);
            Assert.AreEqual(source.Commodity.Id.ToString(), result.Commodity.Identifier.Identifier);
            Assert.AreEqual(source.CommodityUnit, result.CommodityUnit);
            Assert.AreEqual(source.Location.Id.ToString(), result.Location.Identifier.Identifier);
            Assert.AreEqual(source.DefaultSpread, result.DefaultSpread);
		}
    }
}

	
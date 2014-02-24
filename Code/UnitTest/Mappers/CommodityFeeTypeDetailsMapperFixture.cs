namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class CommodityFeeTypeDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new CommodityFeeType()
            {
                Commodity = new Commodity() { Id = 1 },
                FeeType = new FeeType() { Id = 2 },
            };
            var mapper = new MDM.Mappers.CommodityFeeTypeDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Commodity.Id.ToString(), result.Commodity.Identifier.Identifier);
            Assert.AreEqual(source.FeeType.Id.ToString(), result.FeeType.Identifier.Identifier);

		}
    }
}

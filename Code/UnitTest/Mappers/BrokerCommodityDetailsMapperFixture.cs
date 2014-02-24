namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
	using EnergyTrading.MDM.Test;

    [TestClass]
    public class BrokerCommodityDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<BrokerCommodity>();

            var mapper = new MDM.Mappers.BrokerCommodityDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Broker.Id.ToString(), result.Broker.Identifier.Identifier);
            Assert.AreEqual(source.Commodity.Id.ToString(), result.Commodity.Identifier.Identifier);
            
            
		}
    }
}

	
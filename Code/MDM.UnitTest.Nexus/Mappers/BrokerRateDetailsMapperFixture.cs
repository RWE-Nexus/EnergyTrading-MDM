namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class BrokerRateDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<BrokerRateDetails>();

            var mapper = new MDM.Mappers.BrokerRateDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.Broker.Id.ToString(), result.Broker.Identifier.Identifier);
            Assert.AreEqual(source.CommodityInstrumentType.Id.ToString(), result.CommodityInstrumentType.Identifier.Identifier);
            Assert.AreEqual(source.Desk.Id.ToString(), result.Desk.Identifier.Identifier);
            Assert.AreEqual(source.Location.Id.ToString(), result.Location.Identifier.Identifier);
            Assert.AreEqual(source.ProductType.Id.ToString(), result.ProductType.Identifier.Identifier);
            Assert.AreEqual(source.PartyAction, (int)result.PartyAction);
            Assert.AreEqual(source.Rate, result.Rate);
        }
    }
}


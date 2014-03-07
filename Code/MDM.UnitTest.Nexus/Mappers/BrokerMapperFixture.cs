using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnergyTrading.MDM.Mappers;

namespace EnergyTrading.MDM.Test.Mappers
{
    [TestClass]
    public class BrokerMapperFixture
    {
        [TestMethod]
        public void Map_NoConditions_MapsPartyIdToPartyName()
        {
            //Arrange 
            var source = new MDM.Broker() { Party = new Party() { Id = 999 }, PartyRoleType = "Broker"};
            source.Party.AddDetails(new PartyDetails(){ Name = "999" });
            var mapper = new BrokerMapper();

            //Act
            var destination = mapper.Map(source);

            //Assert
            Assert.AreEqual("999", destination.Party.Name);            
        }
    }
}

namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class SettlementContactDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<SettlementContact>();

	        var expected = new RWEST.Nexus.MDM.Contracts.SettlementContactDetails
	        { 
                    Name = source.Name, 
                    CommodityInstrumentType = source.CommodityInstrumentType.CreateNexusEntityId(() => source.CommodityInstrumentType.Id + " (CommodityInstrumentType.Id)"),
                    Location = source.Location.CreateNexusEntityId(() => source.Location.Id + " (Location.Id)")
            };

            var mapper = new MDM.Mappers.SettlementContactDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
		}
    }
}

	

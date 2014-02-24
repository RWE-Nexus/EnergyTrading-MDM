namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class CounterpartyDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<CounterpartyDetails>();

	        var expected = new RWEST.Nexus.MDM.Contracts.CounterpartyDetails
	            {
	                Name = source.Name,
	                Fax = source.Fax,
	                Phone = source.Phone,
	                ShortName = source.ShortName,
            };

            var mapper = new MDM.Mappers.CounterpartyDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
		}
    }
}

	

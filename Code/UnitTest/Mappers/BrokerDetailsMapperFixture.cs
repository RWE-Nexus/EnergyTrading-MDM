namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class BrokerDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<BrokerDetails>();

            var expected = new RWEST.Nexus.MDM.Contracts.BrokerDetails
            {
                Name = source.Name,
                Fax = source.Fax,
                Phone = source.Phone,
                Rate = source.Rate
            };

            var mapper = new MDM.Mappers.BrokerDetailsMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
		}
    }
}

	
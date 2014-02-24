namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.MDM.Test;

    using BusinessUnitDetails = EnergyTrading.MDM.BusinessUnitDetails;

    [TestClass]
    public class BusinessUnitDetailsMapperFixture : Fixture
    {
	    [TestMethod]
        public void Map()
        {
            // Arrange
	        var source = ObjectMother.Create<BusinessUnitDetails>();

            var mapper = new MDM.Mappers.BusinessUnitDetailsMapper();
            var expected = new RWEST.Nexus.MDM.Contracts.BusinessUnitDetails
            {
                Name = source.Name,
                Fax = source.Fax,
                Phone = source.Phone,
                AccountType = source.AccountType,
                Address =  source.Address,
                Status = source.Status,
                TaxLocation = source.TaxLocation.CreateNexusEntityId(() => source.Name)
            };

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Phone, result.Phone);
            Assert.AreEqual(expected.Fax, result.Fax);
            Assert.AreEqual(expected.TaxLocation.Identifier.Identifier, result.TaxLocation.Identifier.Identifier);
		}
    }
}

	
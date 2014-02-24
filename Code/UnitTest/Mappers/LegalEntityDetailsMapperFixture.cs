namespace EnergyTrading.MDM.Test.Mappers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class LegalEntityDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var source = ObjectMother.Create<LegalEntityDetails>();

            var mapper = new MDM.Mappers.LegalEntityDetailsMapper();
            var expected = new RWEST.Nexus.MDM.Contracts.LegalEntityDetails
            {
                Name = source.Name,
                Address = source.Address,
                CountryOfIncorporation = source.CountryOfIncorporation,
                Email = source.Email,
                Fax = source.Fax,
                PartyStatus = source.PartyStatus,
                Phone = source.Phone,
                RegisteredName = source.RegisteredName,
                RegistrationNumber = source.RegistrationNumber,
                Website = source.Website,
                CustomerAddress = source.CustomerAddress,
                InvoiceSetup = source.InvoiceSetup,
                VendorAddress = source.VendorAddress
            };

            var result = mapper.Map(source);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Address, result.Address);
            Assert.AreEqual(expected.CountryOfIncorporation, result.CountryOfIncorporation);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.Fax, result.Fax);
            Assert.AreEqual(expected.PartyStatus, result.PartyStatus);
            Assert.AreEqual(expected.Phone, result.Phone);
            Assert.AreEqual(expected.RegisteredName, result.RegisteredName);
            Assert.AreEqual(expected.RegistrationNumber, result.RegistrationNumber);
            Assert.AreEqual(expected.Website, result.Website);
            Assert.AreEqual(expected.InvoiceSetup, result.InvoiceSetup);
            Assert.AreEqual(expected.CustomerAddress, result.CustomerAddress);
            Assert.AreEqual(expected.VendorAddress, result.VendorAddress);
        }
    }
}
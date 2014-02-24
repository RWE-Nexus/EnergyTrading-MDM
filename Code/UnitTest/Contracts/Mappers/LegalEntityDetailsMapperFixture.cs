namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LegalEntityDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var source = new RWEST.Nexus.MDM.Contracts.LegalEntityDetails
            {
                Name = "Name",
                RegisteredName = "RegisteredName",
                RegistrationNumber = "RegistrationNumber",
                Address = "123 Fake St",
                Website = "http://test.com",
                CountryOfIncorporation = "Germany",
                Email = "foo@bar.com",
                Fax = "020 1234 5678",
                Phone = "020 3469 1256",
                PartyStatus = "Active",
                CustomerAddress = "456 Wrong Road",
                InvoiceSetup = "Customer",
                VendorAddress = "456 Right Road"
            };
            var mapper = new EnergyTrading.MDM.Contracts.Mappers.LegalEntityDetailsMapper();

            var result = mapper.Map(source);

            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Address, result.Address);
            Assert.AreEqual(source.CountryOfIncorporation, result.CountryOfIncorporation);
            Assert.AreEqual(source.Email, result.Email);
            Assert.AreEqual(source.Fax, result.Fax);
            Assert.AreEqual(source.PartyStatus, result.PartyStatus);
            Assert.AreEqual(source.Phone, result.Phone);
            Assert.AreEqual(source.RegisteredName, result.RegisteredName);
            Assert.AreEqual(source.RegistrationNumber, result.RegistrationNumber);
            Assert.AreEqual(source.Website, result.Website);
            Assert.AreEqual(source.CustomerAddress, result.CustomerAddress);
            Assert.AreEqual(source.InvoiceSetup, result.InvoiceSetup);
            Assert.AreEqual(source.VendorAddress, result.VendorAddress);
        }
    }
}

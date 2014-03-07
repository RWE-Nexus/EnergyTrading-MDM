namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class BusinessUnitDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeParty = new MDM.Party();

            mockRepository.Setup(repository => repository.FindOne<MDM.Party>(1)).Returns(fakeParty);
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.BusinessUnitDetails
            {
                Name = "test",
                Fax = "fax",
                Phone = "2345343",
                AccountType = "TRADING",
                Address = "12 Hanover street, Germany",
                Status = "Active",
                TaxLocation = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.BusinessUnitDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Fax, result.Fax);
            Assert.AreEqual(source.Phone, result.Phone);
            Assert.AreEqual(source.AccountType, result.AccountType);
            Assert.AreEqual(source.Address, result.Address);
            Assert.AreEqual(source.Status, result.Status);
        }
    }
}

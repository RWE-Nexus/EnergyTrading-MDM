namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class BookDefaultDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
             var mockRepository = new Mock<IRepository>();
            var fakeBook = new MDM.Book();
            var fakeDesk = new MDM.PartyRole();
            var fakePerson = new MDM.Person();
            var fakePartyRole = new MDM.PartyRole();

            mockRepository.Setup(r => r.FindOne<MDM.Book>(1)).Returns(fakeBook);
            mockRepository.Setup(r => r.FindOne<MDM.PartyRole>(2)).Returns(fakeDesk);
            mockRepository.Setup(r => r.FindOne<MDM.Person>(3)).Returns(fakePerson);
            mockRepository.Setup(r => r.FindOne<MDM.PartyRole>(4)).Returns(fakePartyRole);
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.BookDefaultDetails
                {
                    Name = "test",
                    Desk = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "2" } },
                    Book = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    Trader = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "3" } },
                    GfProductMapping = "GfProductMapping",
                    DefaultType = "Book",
                    PartyRole = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "4" } }
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.BookDefaultDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(fakeBook, result.Book);
            Assert.AreEqual(fakeDesk, result.Desk);
            Assert.AreEqual(fakePerson, result.Trader);
            Assert.AreEqual(source.GfProductMapping, result.GfProductMapping);
            Assert.AreEqual(source.DefaultType, result.DefaultType);
            Assert.AreEqual(fakePartyRole, result.PartyRole);
        }
    }
}
        
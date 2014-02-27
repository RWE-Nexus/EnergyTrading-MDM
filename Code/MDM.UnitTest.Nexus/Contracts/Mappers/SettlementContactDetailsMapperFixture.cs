namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class SettlementContactDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var sourceParty = new MDM.Party();
            var targetParty = new MDM.Party();
            var sourcePerson = new MDM.Person();
            var targetPerson = new MDM.Person();
            var commodityInstrumentType = new MDM.CommodityInstrumentType();
            var location = new MDM.Location();

            mockRepository.Setup(repository => repository.FindOne<MDM.Party>(1)).Returns(sourceParty);
            mockRepository.Setup(repository => repository.FindOne<MDM.Party>(2)).Returns(targetParty);
            mockRepository.Setup(repository => repository.FindOne<MDM.Person>(3)).Returns(sourcePerson);
            mockRepository.Setup(repository => repository.FindOne<MDM.Person>(4)).Returns(targetPerson);
            mockRepository.Setup(repository => repository.FindOne<MDM.CommodityInstrumentType>(5)).Returns(commodityInstrumentType);
            mockRepository.Setup(repository => repository.FindOne<MDM.Location>(6)).Returns(location);

            // Arrange
            var source = new SettlementContactDetails
                {
					Name = "test",
                    SourceParty = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1"}},
                    TargetParty = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "2" } },
                    SourcePerson = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "3" } },
                    TargetPerson = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "4" } },
                    CommodityInstrumentType = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "5" } },
                    Location = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "6" } }
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.SettlementContactDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
			Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual("SettlementContact", result.PartyAccountabilityType);
            Assert.AreSame(sourceParty, result.SourceParty);
            Assert.AreSame(targetParty, result.TargetParty);
            Assert.AreSame(sourcePerson, result.SourcePerson);
            Assert.AreSame(targetPerson, result.TargetPerson);
            Assert.AreSame(commodityInstrumentType, result.CommodityInstrumentType); 
            Assert.AreSame(location, result.Location);
        }
    }
}
		

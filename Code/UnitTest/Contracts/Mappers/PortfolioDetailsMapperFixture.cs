using Moq;
using EnergyTrading.Data;

namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PortfolioDetailsMapperFixture : Fixture
    {
       

        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            // Arrange
            var fakePartyRole = new MDM.PartyRole();

            mockRepository.Setup(repository => repository.FindOne<MDM.PartyRole>(1)).Returns(fakePartyRole);

            var source = new RWEST.Nexus.MDM.Contracts.PortfolioDetails
                {
                    Name = "Test Portfolio",
                    PortfolioType = "Test Portfolio Type"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.PortfolioDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Portfolio", result.Name);
            Assert.AreEqual("Test Portfolio Type", result.PortfolioType);
        }
    }
}
		
namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class PortfolioHierarchyDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakePortfolio = new MDM.Portfolio();
            var fakeHierarchy = new MDM.Hierarchy();

            mockRepository.Setup(repository => repository.FindOne<MDM.Hierarchy>(1)).Returns(fakeHierarchy);
            mockRepository.Setup(repository => repository.FindOne<MDM.Portfolio>(1)).Returns(fakePortfolio);
            mockRepository.Setup(repository => repository.FindOne<MDM.Portfolio>(1)).Returns(fakePortfolio);

            var source = new RWEST.Nexus.MDM.Contracts.PortfolioHierarchyDetails() 
            {
                Hierarchy = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                ChildPortfolio = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                ParentPortfolio = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
            };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.PortfolioHierarchyDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeHierarchy, result.Hierarachy);
            Assert.AreEqual(fakePortfolio, result.ChildPortfolio);
            Assert.AreEqual(fakePortfolio, result.ParentPortfolio);
        }
    }
}
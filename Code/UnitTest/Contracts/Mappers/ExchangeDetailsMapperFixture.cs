namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;

    [TestClass]
    public class ExchangeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var mockRepository = new Mock<IRepository>();
            var fakeParty = new MDM.Party();

            mockRepository.Setup(repository => repository.FindOne<MDM.Party>(1)).Returns(fakeParty);
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.ExchangeDetails
                {
					Name = "test"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ExchangeDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
			Assert.AreEqual(source.Name, result.Name);
        }
    }
}
		

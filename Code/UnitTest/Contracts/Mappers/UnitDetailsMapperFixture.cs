namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class UnitDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeDimension = new MDM.Dimension();

            mockRepository.Setup(x => x.FindOne<MDM.Dimension>(1)).Returns(fakeDimension);


            var source = new RWEST.Nexus.MDM.Contracts.UnitDetails
                {
                    Name = "testUnit",
                    Description = "Thermal unit",
                    ConversionConstant = new decimal(2.3),
                    ConversionFactor = new decimal(5.3000045),
                    Dimension = new EntityId { Identifier = new NexusId { IsNexusId = true, Identifier = "1" } },
                    Symbol = "£<=>$"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.UnitDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Description, result.Description);
            Assert.AreEqual(source.ConversionConstant, result.ConversionConstant);
            Assert.AreEqual(source.ConversionFactor, result.ConversionFactor);
            Assert.AreEqual(source.Symbol, result.Symbol);
            Assert.AreEqual(fakeDimension, result.Dimension);
        }
    }
}
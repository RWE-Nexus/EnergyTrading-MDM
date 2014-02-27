namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FeeTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.FeeTypeDetails
                {
					// TODO_UnitTestGeneration_FeeType - populate properties of entity
					// Name = "test"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.FeeTypeDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
			// TODO_UnitTestGeneration_FeeType - assert properties are mapped correctly
			// Assert.AreEqual(source.Name, result.Name);
        }
    }
}
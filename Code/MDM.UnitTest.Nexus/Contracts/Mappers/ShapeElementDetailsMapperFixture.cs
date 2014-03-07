namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using EnergyTrading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.Extensions;

    [TestClass]
    public class ShapeElementDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = new RWEST.Nexus.MDM.Contracts.ShapeElementDetails
                {
                    Name = "test",
                    Period = DateRange.MaxDateRange.ToContract()
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.ShapeElementDetailsMapper();

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Period.StartDate, result.Period.Start);
            Assert.AreEqual(source.Period.EndDate, result.Period.Finish);
        }
    }
}

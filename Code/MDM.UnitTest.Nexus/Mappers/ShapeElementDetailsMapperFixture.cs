using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Test.Mappers
{
    [TestClass]
    public class ShapeElementDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<ShapeElement>();

            var mapper = new MDM.Mappers.ShapeElementDetailsMapper();

            var expected = new RWEST.Nexus.MDM.Contracts.ShapeElementDetails()
            {
                Name = source.Name,
                Period = new RWEST.Nexus.MDM.Contracts.DateRange
                             {
                                 StartDate = DateTime.Today,
                                 EndDate = DateTime.Today.AddDays(1)
                             }
            };

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Period.Start, result.Period.StartDate);
            Assert.AreEqual(source.Period.Finish, result.Period.EndDate);
        }
    }
}
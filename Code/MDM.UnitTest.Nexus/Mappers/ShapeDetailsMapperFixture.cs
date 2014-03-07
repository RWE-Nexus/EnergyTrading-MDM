using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnergyTrading.MDM.Test.Mappers
{
    [TestClass]
    public class ShapeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var source = ObjectMother.Create<Shape>();

            var mapper = new MDM.Mappers.ShapeDetailsMapper();

            var expected = new RWEST.Nexus.MDM.Contracts.ShapeDetails()
                               {
                                   Name = source.Name
                               };


            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Check(expected, result);
        }
    }
}
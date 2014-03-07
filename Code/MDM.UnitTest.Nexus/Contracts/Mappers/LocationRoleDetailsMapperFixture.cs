namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data;

    [TestClass]
    public class LocationRoleDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeLocation = new MDM.Location();
            var fakeLocationRole = new MDM.LocationRoleType();
            var roleList = new List<MDM.LocationRoleType> { fakeLocationRole };

            mockRepository.Setup(repository => repository.FindOne<MDM.Location>(1)).Returns(fakeLocation);
            mockRepository.Setup(repository => repository.Queryable<MDM.LocationRoleType>()).Returns(roleList.AsQueryable());

            var source = new RWEST.Nexus.MDM.Contracts.LocationRoleDetails
                {
                    Location = new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } },
                    Type = fakeLocationRole.Name,
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.LocationRoleDetailsMapper(mockRepository.Object);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeLocation, result.Location);
            Assert.AreEqual(fakeLocationRole, result.Type);
        }
    }
}
		
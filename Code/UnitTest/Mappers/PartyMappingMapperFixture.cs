namespace EnergyTrading.MDM.Test.Mappers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading;
    using EnergyTrading.MDM.Mappers;
    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyMappingMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var system = new SourceSystem { Name = "Test" };
            var start = new DateTime(2010, 1, 1);
            var end = new DateTime(2012, 12, 31);

            var source = new PartyMapping
                {
                    Id = 100,
                    System = system,
                    MappingValue = "1",
                    IsMaster = true,
                    Validity = new DateRange(start, end)
                };
            var expected = new RWEST.Nexus.MDM.Contracts.NexusId
                {
                    MappingId = 100,
                    SystemName = "Test",
                    Identifier = "1",
                    SourceSystemOriginated = true,
                    StartDate = start,
                    EndDate = end
                };

            var mapper = new PartyMappingMapper();

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}
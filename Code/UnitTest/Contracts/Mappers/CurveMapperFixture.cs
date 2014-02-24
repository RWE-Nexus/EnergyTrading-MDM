namespace RWEST.Nexus.MDM.Test.Contracts.Mappers
{
    using System;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Configuration;
    using RWEST.Nexus.MDM.Contracts.Mappers;
    using RWEST.Nexus;
    using RWEST.Nexus.Mapping;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CurveMapperFixture : Fixture
    {
        [TestMethod]
        public void Resolve()
        {
            var container = CreateContainer();
            var meConfig = new SimpleMappingEngineConfiguration(container);
            meConfig.Configure();

            var config = new CurveConfiguration(container);
            config.Configure();

            var validator = container.Resolve<IMapper<MDM.Contracts.Curve, Curve>>();

            // Assert
            Assert.IsNotNull(validator, "Mapper resolution failed");
        }

        [TestMethod]
        public void Map()
        {
            // Arrange
            var start = new DateTime(2010, 1, 1);
            var end = DateUtility.MaxDate;
            var range = new DateRange(start, end);

            var id = new MDM.Contracts.NexusId { SystemName = "Test", Identifier = "A" };
            var contractDetails = new MDM.Contracts.CurveDetails();
            var contract = new MDM.Contracts.Curve
            {
                Identifiers = new MDM.Contracts.NexusIdList { id },
                Details = contractDetails,
                Nexus = new MDM.Contracts.SystemData { StartDate = start, EndDate = end }
            };

            // NB Don't assign validity here, want to prove SUT sets it
            var details = new Curve();

            var mapping = new CurveMapping();

            var mappingEngine = new Mock<IMappingEngine>();
            mappingEngine.Setup(x => x.Map<MDM.Contracts.NexusId, CurveMapping>(id)).Returns(mapping);
            mappingEngine.Setup(x => x.Map<MDM.Contracts.CurveDetails, Curve>(contractDetails)).Returns(details);

            var mapper = new CurveMapper(mappingEngine.Object);

            // Act
            var candidate = mapper.Map(contract);

            // Assert
            //Assert.AreEqual(1, candidate.Details.Count, "Detail count differs");
            Assert.AreEqual(1, candidate.Mappings.Count, "Mapping count differs");
            Check(range, details.Validity, "Validity differs");
        }
    }
}

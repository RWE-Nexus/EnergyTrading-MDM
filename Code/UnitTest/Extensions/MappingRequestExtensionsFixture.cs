namespace EnergyTrading.MDM.Test.Extensions
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Messages;

    [TestClass]
    public class MappingRequestExtensionsFixture
    {
        [TestMethod]
        public void IsNexusMappingRequest_EmptySourceSystem_ReturnsFalse()
        {
            var mappingRequest = new MappingRequest();
            Assert.IsFalse(mappingRequest.IsNexusMappingRequest());
        }

        [TestMethod]
        public void IsNexusMappingRequest_NexusSourceSystem_ReturnsTrue()
        {
            var mappingRequest = new MappingRequest { SystemName = SourceSystemNames.Nexus, Identifier = "999" };
            Assert.IsTrue(mappingRequest.IsNexusMappingRequest());
        }

        [TestMethod]
        public void HasNumericIdentifier_WithInteger_ReturnsTrue()
        {
            var mappingRequest = new MappingRequest { SystemName = SourceSystemNames.Nexus, Identifier = "999" };
            Assert.IsTrue(mappingRequest.HasNumericIdentifier());
        }

        [TestMethod]
        public void HasNumericIdentifier_WithNaN_ReturnsTrue()
        {
            var mappingRequest = new MappingRequest { SystemName = SourceSystemNames.Nexus, Identifier = "string" };
            Assert.IsFalse(mappingRequest.HasNumericIdentifier());
        }
    }
}

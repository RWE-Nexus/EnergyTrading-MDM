namespace EnergyTrading.MDM.Test.Extensions
{
    using EnergyTrading.MDM.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_mapping_response_has_more_than_one_default
    {
        [TestMethod]
        public void HasMultipleDefaultMapping_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new NexusIdList() };
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = true });
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = true });

            Assert.IsTrue(mappingResponse.HasMultipleDefaultMapping());
        }
    }

    [TestClass]
    public class when_a_mapping_response_has_one_default
    {
        [TestMethod]
        public void HasMultipleDefaultMapping_should_return_false()
        {
            var mappingResponse = new MappingResponse() { Mappings = new NexusIdList() };
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = true });
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = false });        
            Assert.IsFalse(mappingResponse.HasMultipleDefaultMapping());
        }
    }

    [TestClass]
    public class when_a_mapping_response_has_multiple_mappings_with_no_default
    {
        [TestMethod]
        public void HasMultipleMappingsWithNoDefault_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new NexusIdList() };
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = false });
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = false });        
            Assert.IsTrue(mappingResponse.HasMultipleMappingsWithNoDefault());
        }
    }

    [TestClass]
    public class when_a_mapping_response_has_multiple_mappings_with_one_default
    {
        [TestMethod]
        public void HasMutlipleMappingsWithOneDefault_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new NexusIdList() };
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = false });
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = false });        
            mappingResponse.Mappings.Add(new NexusId() { DefaultReverseInd = true });        
            Assert.IsTrue(mappingResponse.HasMutlipleMappingsWithOneDefault());
        }
    }
}
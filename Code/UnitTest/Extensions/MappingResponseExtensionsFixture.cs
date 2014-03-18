namespace EnergyTrading.MDM.Test.Extensions
{
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Mdm.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_mapping_response_has_more_than_one_default
    {
        [TestMethod]
        public void HasMultipleDefaultMapping_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });

            Assert.IsTrue(mappingResponse.HasMultipleDefaultMapping());
        }
    }

    [TestClass]
    public class when_a_mapping_response_has_one_default
    {
        [TestMethod]
        public void HasMultipleDefaultMapping_should_return_false()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });        
            Assert.IsFalse(mappingResponse.HasMultipleDefaultMapping());
        }
    }

    [TestClass]
    public class when_a_mapping_response_has_multiple_mappings_with_no_default
    {
        [TestMethod]
        public void HasMultipleMappingsWithNoDefault_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });        
            Assert.IsTrue(mappingResponse.HasMultipleMappingsWithNoDefault());
        }
    }

    [TestClass]
    public class when_a_mapping_response_has_multiple_mappings_with_one_default
    {
        [TestMethod]
        public void HasMutlipleMappingsWithOneDefault_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });        
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });        
            Assert.IsTrue(mappingResponse.HasMutlipleMappingsWithOneDefault());
        }
    }
}
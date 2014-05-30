namespace EnergyTrading.MDM.Test.Extensions
{
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class when_a_mapping_response_has_more_than_one_default
    {
        [Test]
        public void HasMultipleDefaultMapping_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });

            Assert.IsTrue(mappingResponse.HasMultipleDefaultMapping());
        }
    }

    [TestFixture]
    public class when_a_mapping_response_has_one_default
    {
        [Test]
        public void HasMultipleDefaultMapping_should_return_false()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = true });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });        
            Assert.IsFalse(mappingResponse.HasMultipleDefaultMapping());
        }
    }

    [TestFixture]
    public class when_a_mapping_response_has_multiple_mappings_with_no_default
    {
        [Test]
        public void HasMultipleMappingsWithNoDefault_should_return_true()
        {
            var mappingResponse = new MappingResponse() { Mappings = new MdmIdList() };
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });
            mappingResponse.Mappings.Add(new MdmId() { DefaultReverseInd = false });        
            Assert.IsTrue(mappingResponse.HasMultipleMappingsWithNoDefault());
        }
    }

    [TestFixture]
    public class when_a_mapping_response_has_multiple_mappings_with_one_default
    {
        [Test]
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
namespace EnergyTrading.MDM.Test 
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_for_sourcesystem : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MDM.SourceSystem entity;
        private static MappingResponse mappingResponse;
        private static HttpClient client;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            entity = Script.SourceSystemData.CreateEntityWithTwoDetailsAndTwoMappings();

            client = new HttpClient(ServiceUrl["SourceSystem"] +
                "crossmap?source-system=trayport&destination-system=endur&mapping-string=" + entity.Mappings[0].MappingValue);

            response = client.Get();

            mappingResponse = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.MappingResponse>();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_mapping()
        {
            Assert.AreEqual(entity.Mappings[1].Validity.Start, mappingResponse.Mappings[0].StartDate);
            Assert.AreEqual(entity.Mappings[1].Validity.Finish, mappingResponse.Mappings[0].EndDate);
            Assert.AreEqual("endur", mappingResponse.Mappings[0].SystemName.ToLower());
            Assert.IsFalse(mappingResponse.Mappings[0].IsMdmId);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }

        [Test]
        public void should_return_only_one_mapping()
        {
            Assert.AreEqual(1, mappingResponse.Mappings.Count);
        }
    }

    [TestFixture]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_as_of_a_date_for_sourcesystem : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MappingResponse mappingResponse;
        private static HttpClient client;
        private static MDM.SourceSystem entity;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            entity = Script.SourceSystemData.CreateEntityWithTwoDetailsAndTwoMappings();

            client = new HttpClient(ServiceUrl["SourceSystem"] +
                "crossmap?source-system=trayport&destination-system=endur&mapping-string=" + entity.Mappings[0].MappingValue
                + "&as-of=" + entity.Mappings[0].Validity.Start.ToString(DateFormatString));

            response = client.Get();
            mappingResponse = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.MappingResponse>();
        }

        [Test]
        public void should_return_the_correct_vesrion_of_the_sourcesystem()
        {
            Assert.AreEqual(entity.Mappings[1].Validity.Start, mappingResponse.Mappings[0].StartDate);
            Assert.AreEqual(entity.Mappings[1].Validity.Finish, mappingResponse.Mappings[0].EndDate);
            Assert.AreEqual("endur", mappingResponse.Mappings[0].SystemName.ToLower());
            Assert.IsFalse(mappingResponse.Mappings[0].IsMdmId);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }

        [Test]
        public void should_return_only_one_mapping()
        {
            Assert.AreEqual(1, mappingResponse.Mappings.Count);
        }
    }
}
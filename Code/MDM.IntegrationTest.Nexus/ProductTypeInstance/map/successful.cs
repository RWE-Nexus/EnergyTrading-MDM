namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Data.SqlTypes;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_as_of_a_specific_date_producttypeinstance  : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static ProductTypeInstanceDetails firstDetails;
        private static HttpClient client;
        private static MDM.ProductTypeInstance producttypeinstance;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            producttypeinstance = Script.ProductTypeInstanceData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["ProductTypeInstance"] +
                    "map?source-system=Trayport&mapping-string=" + producttypeinstance.Mappings[0].MappingValue + "&as-of=" +
                    producttypeinstance.Validity.Start.ToString(DateFormatString));

            response = client.Get();
        }

        [TestMethod]
        public void should_return_the_correct_vesrion_of_the_producttypeinstance()
        {
            var producttypeinstance = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTypeInstance>();

            Script.ProductTypeInstanceDataChecker.CompareContractWithSavedEntity(producttypeinstance);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }
    }

    [TestClass]
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_as_of_now_for_producttypeinstance : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;
        private static MDM.ProductTypeInstance producttypeinstance;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            producttypeinstance = Script.ProductTypeInstanceData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["ProductTypeInstance"] +
                "map?source-system=Trayport&mapping-string=" + producttypeinstance.Mappings[0].MappingValue);

            response = client.Get();
        }

        [TestMethod]
        public void should_return_the_correct_vesrion_of_the_producttypeinstance()
        {
            var producttypeinstance = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTypeInstance>();

            Script.ProductTypeInstanceDataChecker.CompareContractWithSavedEntity(producttypeinstance);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }
    }
}
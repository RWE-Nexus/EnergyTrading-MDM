namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_to_create_a_curve_entity_and_the_xml_does_not_satisfy_the_schema : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            var notAPerson = new Mapping();
            content = HttpContentExtensions.CreateDataContract(notAPerson);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(new Uri(ServiceUrl["Person"]), content);
        }

        [TestMethod]
        public void should_return_bad_request_status_code()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}
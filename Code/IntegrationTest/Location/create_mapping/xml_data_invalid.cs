namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_create_a_location_mapping_and_the_xml_does_not_satisfy_the_schema : IntegrationTestBase
    {
        private static HttpResponseMessage response;

        private static HttpContent content;

        private static HttpClient client;

        private static MDM.Location entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = Script.LocationData.CreateBasicEntity();
            var notAMapping = new RWEST.Nexus.MDM.Contracts.Location();
            content = HttpContentExtensions.CreateDataContract(notAMapping);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["Location"] + string.Format("{0}/Mapping", entity.Id), content);
        }

        [TestMethod]
        public void should_return_bad_request_status_code()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
}

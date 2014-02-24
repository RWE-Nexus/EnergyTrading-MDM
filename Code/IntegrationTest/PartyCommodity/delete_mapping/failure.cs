namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_delete_a_partycommodity_mapping : IntegrationTestBase
    {
        private static HttpClient client;
        private static HttpResponseMessage response;
 
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            var uri = ServiceUrl["PartyOverride"] + "1/Mapping/1";
            response = client.Delete(uri);
        }

        [TestMethod]
        public void should_return_error()
        {
            response.AssertStatusCodeIs(HttpStatusCode.InternalServerError); 
        }
    }
}
    
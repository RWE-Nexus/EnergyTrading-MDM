namespace EnergyTrading.MDM.Test 
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_for_partycommodity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["PartyOverride"] + "crossmap?source-system=trayport&destination-system=endur&mapping-string=test");
            response = client.Get();
        }

        [TestMethod]
        public void should_return_not_found()
        {
            response.AssertStatusCodeIs(HttpStatusCode.NotFound);
        }
   }
}
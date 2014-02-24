namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_source_system_to_master_data_service_mapping_request_is_made_partycommodity : IntegrationTestBase
    {
        private static HttpClient client;
        private static HttpResponseMessage response;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["PartyCommodity"] + "map?source-system=Trayport&mapping-string=test");
            response = client.Get();
        }

        [TestMethod]
        public void should_return_not_found()
        {
            response.AssertStatusCodeIs(HttpStatusCode.NotFound);
        }
    }
}

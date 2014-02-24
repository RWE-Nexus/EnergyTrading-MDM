namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_update_a_vessel_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static RWEST.Nexus.MDM.Contracts.Vessel vesselDataContract;
        private static HttpClient client;
        private static MDM.Vessel entity;

        private static RWEST.Nexus.MDM.Contracts.Vessel updatedContract;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = VesselData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Vessel"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Vessel>();
            content = HttpContentExtensions.CreateDataContract(VesselData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["Vessel"] + entity.Id, content);
            response.AssertStatusCodeIs(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void should_update_the_vessel_in_the_database_with_the_correct_details()
        {
            VesselDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

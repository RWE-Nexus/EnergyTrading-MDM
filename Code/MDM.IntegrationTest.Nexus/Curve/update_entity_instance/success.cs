namespace RWEST.Nexus.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_update_a_curve_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static Contracts.Curve curveDataContract;
        private static HttpClient client;
        private static MDM.Curve entity;

        private static Contracts.Curve updatedContract;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = CurveData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Curve"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<Contracts.Curve>();
            content = HttpContentExtensions.CreateDataContract(CurveData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["Curve"] + entity.Id, content);
        }

        [TestMethod]
        public void should_update_the_curve_in_the_database_with_the_correct_details()
        {
            CurveDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

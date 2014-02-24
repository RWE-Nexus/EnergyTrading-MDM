namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_update_a_producttypeinstance_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static RWEST.Nexus.MDM.Contracts.ProductTypeInstance producttypeinstanceDataContract;
        private static HttpClient client;
        private static MDM.ProductTypeInstance entity;

        private static RWEST.Nexus.MDM.Contracts.ProductTypeInstance updatedContract;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.ProductTypeInstanceData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["ProductTypeInstance"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTypeInstance>();
            content = HttpContentExtensions.CreateDataContract(Script.ProductTypeInstanceData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["ProductTypeInstance"] + entity.Id, content);
        }

        [TestMethod]
        public void should_update_the_producttypeinstance_in_the_database_with_the_correct_details()
        {
            Script.ProductTypeInstanceDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

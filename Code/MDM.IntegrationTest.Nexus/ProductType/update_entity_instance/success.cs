namespace EnergyTrading.MDM.Test
{
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_update_a_producttype_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static RWEST.Nexus.MDM.Contracts.ProductType producttypeDataContract;
        private static HttpClient client;
        private static MDM.ProductType entity;

        private static RWEST.Nexus.MDM.Contracts.ProductType updatedContract;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.ProductTypeData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["ProductType"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductType>();
            content = HttpContentExtensions.CreateDataContract(Script.ProductTypeData.MakeChangeToContract(updatedContract));
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["ProductType"] + entity.Id, content);
        }

        [TestMethod]
        public void should_update_the_producttype_in_the_database_with_the_correct_details()
        {
            Script.ProductTypeDataChecker.ConfirmEntitySaved(entity.Id, updatedContract);
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_to_create_a_productcurve_entity : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static RWEST.Nexus.MDM.Contracts.ProductCurve productcurve;
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
            productcurve = ProductCurveData.CreateContractForEntityCreation();

            content = HttpContentExtensions.CreateDataContract(productcurve);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            response = client.Post(ServiceUrl["ProductCurve"], content);
            response.AssertStatusCodeIs(HttpStatusCode.Created);
        }

        [TestMethod]
        public void should_create_an_instance_of_the_productcurve_in_the_database_with_the_correct_details()
        {
            ProductCurveDataChecker.ConfirmEntitySaved(int.Parse(GetLocationHeader()[1]), productcurve);
        }

        [TestMethod]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void should_return_the_location_of_the_entity()
        {
            int id;
            bool parsedInt = int.TryParse(GetLocationHeader()[1], out id);
            Assert.IsTrue(parsedInt, "The id returned was not an integer");
        }

        private string[] GetLocationHeader()
        {
            return response.Headers["Location"].Substring(0, response.Headers["Location"].IndexOf('?')).Split('/');
        }
    }
}
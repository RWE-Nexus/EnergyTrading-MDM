namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_product_and_they_exist : IntegrationTestBase
    {
        private static MDM.Product product;

        private static RWEST.Nexus.MDM.Contracts.Product returnedProduct;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            product = Script.ProductData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Product"] + 
                product.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProduct = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Product>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_product_with_the_correct_details()
        {
            Script.ProductDataChecker.CompareContractWithSavedEntity(returnedProduct);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_product_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Product product;
        private static RWEST.Nexus.MDM.Contracts.Product returnedProduct;
        private static DateTime asof;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            product = Script.ProductData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Product"] + string.Format("{0}?as-of={1}",
                    product.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedProduct = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Product>();
        }

        [TestMethod]
        public void should_return_the_product_with_the_correct_details()
        {
            Script.ProductDataChecker.CompareContractWithSavedEntity(returnedProduct);
        }
    }
}
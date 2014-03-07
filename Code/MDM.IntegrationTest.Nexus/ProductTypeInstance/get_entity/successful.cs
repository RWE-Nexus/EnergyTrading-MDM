namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_producttypeinstance_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductTypeInstance producttypeinstance;

        private static RWEST.Nexus.MDM.Contracts.ProductTypeInstance returnedProductTypeInstance;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            producttypeinstance = Script.ProductTypeInstanceData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ProductTypeInstance"] + 
                producttypeinstance.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProductTypeInstance = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTypeInstance>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_producttypeinstance_with_the_correct_details()
        {
            Script.ProductTypeInstanceDataChecker.CompareContractWithSavedEntity(returnedProductTypeInstance);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_producttypeinstance_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductTypeInstance producttypeinstance;
        private static RWEST.Nexus.MDM.Contracts.ProductTypeInstance returnedProductTypeInstance;
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
            producttypeinstance = Script.ProductTypeInstanceData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ProductTypeInstance"] + string.Format("{0}?as-of={1}",
                    producttypeinstance.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedProductTypeInstance = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTypeInstance>();
        }

        [TestMethod]
        public void should_return_the_producttypeinstance_with_the_correct_details()
        {
            Script.ProductTypeInstanceDataChecker.CompareContractWithSavedEntity(returnedProductTypeInstance);
        }
    }
}
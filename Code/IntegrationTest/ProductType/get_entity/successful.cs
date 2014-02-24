namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_producttype_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductType producttype;

        private static RWEST.Nexus.MDM.Contracts.ProductType returnedProductType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            producttype = Script.ProductTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ProductType"] + 
                producttype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProductType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_producttype_with_the_correct_details()
        {
            Script.ProductTypeDataChecker.CompareContractWithSavedEntity(returnedProductType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_producttype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductType producttype;
        private static RWEST.Nexus.MDM.Contracts.ProductType returnedProductType;
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
            producttype = Script.ProductTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ProductType"] + string.Format("{0}?as-of={1}",
                    producttype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedProductType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductType>();
        }

        [TestMethod]
        public void should_return_the_producttype_with_the_correct_details()
        {
            Script.ProductTypeDataChecker.CompareContractWithSavedEntity(returnedProductType);
        }
    }
}
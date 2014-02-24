namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_producttenortype_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductTenorType producttenortype;

        private static RWEST.Nexus.MDM.Contracts.ProductTenorType returnedProductTenorType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            producttenortype = ProductTenorTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ProductTenorType"] + 
                producttenortype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProductTenorType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTenorType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_producttenortype_with_the_correct_details()
        {
            ProductTenorTypeDataChecker.CompareContractWithSavedEntity(returnedProductTenorType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_producttenortype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductTenorType producttenortype;
        private static RWEST.Nexus.MDM.Contracts.ProductTenorType returnedProductTenorType;
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
            producttenortype = ProductTenorTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ProductTenorType"] + string.Format("{0}?as-of={1}",
                    producttenortype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedProductTenorType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductTenorType>();
        }

        [TestMethod]
        public void should_return_the_producttenortype_with_the_correct_details()
        {
            ProductTenorTypeDataChecker.CompareContractWithSavedEntity(returnedProductTenorType);
        }
    }
}
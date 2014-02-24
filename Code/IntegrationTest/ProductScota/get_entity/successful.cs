namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_productscota_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductScota productscota;

        private static RWEST.Nexus.MDM.Contracts.ProductScota returnedProductScota;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            productscota = ProductScotaData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ProductScota"] + 
                productscota.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProductScota = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductScota>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_productscota_with_the_correct_details()
        {
            ProductScotaDataChecker.CompareContractWithSavedEntity(returnedProductScota);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_productscota_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductScota productscota;
        private static RWEST.Nexus.MDM.Contracts.ProductScota returnedProductScota;
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
            productscota = ProductScotaData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ProductScota"] + string.Format("{0}?as-of={1}",
                    productscota.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedProductScota = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductScota>();
        }

        [TestMethod]
        public void should_return_the_productscota_with_the_correct_details()
        {
            ProductScotaDataChecker.CompareContractWithSavedEntity(returnedProductScota);
        }
    }
}
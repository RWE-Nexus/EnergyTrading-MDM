namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_productcurve_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductCurve productcurve;

        private static RWEST.Nexus.MDM.Contracts.ProductCurve returnedProductCurve;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            productcurve = ProductCurveData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ProductCurve"] + 
                productcurve.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProductCurve = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductCurve>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_productcurve_with_the_correct_details()
        {
            ProductCurveDataChecker.CompareContractWithSavedEntity(returnedProductCurve);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_productcurve_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.ProductCurve productcurve;
        private static RWEST.Nexus.MDM.Contracts.ProductCurve returnedProductCurve;
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
            productcurve = ProductCurveData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["ProductCurve"] + string.Format("{0}?as-of={1}",
                    productcurve.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedProductCurve = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.ProductCurve>();
        }

        [TestMethod]
        public void should_return_the_productcurve_with_the_correct_details()
        {
            ProductCurveDataChecker.CompareContractWithSavedEntity(returnedProductCurve);
        }
    }
}
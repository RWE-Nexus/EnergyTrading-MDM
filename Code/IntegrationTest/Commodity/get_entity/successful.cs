namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_commodity_and_they_exist : IntegrationTestBase
    {
        private static MDM.Commodity commodity;

        private static RWEST.Nexus.MDM.Contracts.Commodity returnedCommodity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            commodity = Script.CommodityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Commodity"] + 
                commodity.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCommodity = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Commodity>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_commodity_with_the_correct_details()
        {
            Script.CommodityDataChecker.CompareContractWithSavedEntity(returnedCommodity);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_commodity_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Commodity commodity;
        private static RWEST.Nexus.MDM.Contracts.Commodity returnedCommodity;
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
            commodity = Script.CommodityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Commodity"] + string.Format("{0}?as-of={1}",
                    commodity.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedCommodity = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Commodity>();
        }

        [TestMethod]
        public void should_return_the_commodity_with_the_correct_details()
        {
            Script.CommodityDataChecker.CompareContractWithSavedEntity(returnedCommodity);
        }
    }
}
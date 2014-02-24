namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_commodityfeetype_and_they_exist : IntegrationTestBase
    {
        private static MDM.CommodityFeeType commodityfeetype;

        private static RWEST.Nexus.MDM.Contracts.CommodityFeeType returnedCommodityFeeType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            commodityfeetype = CommodityFeeTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["CommodityFeeType"] + 
                commodityfeetype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCommodityFeeType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.CommodityFeeType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_commodityfeetype_with_the_correct_details()
        {
            CommodityFeeTypeDataChecker.CompareContractWithSavedEntity(returnedCommodityFeeType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_commodityfeetype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.CommodityFeeType commodityfeetype;
        private static RWEST.Nexus.MDM.Contracts.CommodityFeeType returnedCommodityFeeType;
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
            commodityfeetype = CommodityFeeTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["CommodityFeeType"] + string.Format("{0}?as-of={1}",
                    commodityfeetype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedCommodityFeeType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.CommodityFeeType>();
        }

        [TestMethod]
        public void should_return_the_commodityfeetype_with_the_correct_details()
        {
            CommodityFeeTypeDataChecker.CompareContractWithSavedEntity(returnedCommodityFeeType);
        }
    }
}
namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_commodityinstrumenttype_and_they_exist : IntegrationTestBase
    {
        private static MDM.CommodityInstrumentType commodityinstrumenttype;

        private static RWEST.Nexus.MDM.Contracts.CommodityInstrumentType returnedCommodityInstrumentType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            commodityinstrumenttype = CommodityInstrumentTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["CommodityInstrumentType"] + 
                commodityinstrumenttype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCommodityInstrumentType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_commodityinstrumenttype_with_the_correct_details()
        {
            CommodityInstrumentTypeDataChecker.CompareContractWithSavedEntity(returnedCommodityInstrumentType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_commodityinstrumenttype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.CommodityInstrumentType commodityinstrumenttype;
        private static RWEST.Nexus.MDM.Contracts.CommodityInstrumentType returnedCommodityInstrumentType;
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
            commodityinstrumenttype = CommodityInstrumentTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["CommodityInstrumentType"] + string.Format("{0}?as-of={1}",
                    commodityinstrumenttype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedCommodityInstrumentType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.CommodityInstrumentType>();
        }

        [TestMethod]
        public void should_return_the_commodityinstrumenttype_with_the_correct_details()
        {
            CommodityInstrumentTypeDataChecker.CompareContractWithSavedEntity(returnedCommodityInstrumentType);
        }
    }
}
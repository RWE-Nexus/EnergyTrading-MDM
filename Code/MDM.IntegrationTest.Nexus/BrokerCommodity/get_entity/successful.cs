namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_brokercommodity_and_they_exist : IntegrationTestBase
    {
        private static MDM.BrokerCommodity brokercommodity;

        private static RWEST.Nexus.MDM.Contracts.BrokerCommodity returnedBrokerCommodity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            brokercommodity = BrokerCommodityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["BrokerCommodity"] + 
                brokercommodity.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBrokerCommodity = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.BrokerCommodity>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_brokercommodity_with_the_correct_details()
        {
            BrokerCommodityDataChecker.CompareContractWithSavedEntity(returnedBrokerCommodity);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_brokercommodity_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.BrokerCommodity brokercommodity;
        private static RWEST.Nexus.MDM.Contracts.BrokerCommodity returnedBrokerCommodity;
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
            brokercommodity = BrokerCommodityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["BrokerCommodity"] + string.Format("{0}?as-of={1}",
                    brokercommodity.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedBrokerCommodity = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.BrokerCommodity>();
        }

        [TestMethod]
        public void should_return_the_brokercommodity_with_the_correct_details()
        {
            BrokerCommodityDataChecker.CompareContractWithSavedEntity(returnedBrokerCommodity);
        }
    }
}
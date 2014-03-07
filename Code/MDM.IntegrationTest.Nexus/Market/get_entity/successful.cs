namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_market_and_they_exist : IntegrationTestBase
    {
        private static MDM.Market market;

        private static RWEST.Nexus.MDM.Contracts.Market returnedMarket;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            market = Script.MarketData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Market"] + 
                market.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedMarket = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Market>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_market_with_the_correct_details()
        {
            Script.MarketDataChecker.CompareContractWithSavedEntity(returnedMarket);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_market_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Market market;
        private static RWEST.Nexus.MDM.Contracts.Market returnedMarket;
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
            market = Script.MarketData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Market"] + string.Format("{0}?as-of={1}",
                    market.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedMarket = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Market>();
        }

        [TestMethod]
        public void should_return_the_market_with_the_correct_details()
        {
            Script.MarketDataChecker.CompareContractWithSavedEntity(returnedMarket);
        }
    }
}
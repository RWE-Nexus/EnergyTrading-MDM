namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_exchange_and_they_exist : IntegrationTestBase
    {
        private static MDM.Exchange exchange;

        private static RWEST.Nexus.MDM.Contracts.Exchange returnedExchange;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            exchange = ExchangeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Exchange"] + 
                exchange.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedExchange = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Exchange>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_exchange_with_the_correct_details()
        {
            ExchangeDataChecker.CompareContractWithSavedEntity(returnedExchange);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_exchange_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Exchange exchange;
        private static RWEST.Nexus.MDM.Contracts.Exchange returnedExchange;
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
            exchange = ExchangeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Exchange"] + string.Format("{0}?as-of={1}",
                    exchange.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedExchange = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Exchange>();
        }

        [TestMethod]
        public void should_return_the_exchange_with_the_correct_details()
        {
            ExchangeDataChecker.CompareContractWithSavedEntity(returnedExchange);
        }
    }
}
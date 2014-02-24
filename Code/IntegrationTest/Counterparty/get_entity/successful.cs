namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_counterparty_and_they_exist : IntegrationTestBase
    {
        private static MDM.Counterparty counterparty;

        private static RWEST.Nexus.MDM.Contracts.Counterparty returnedCounterparty;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            counterparty = CounterpartyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Counterparty"] + 
                counterparty.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCounterparty = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Counterparty>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_counterparty_with_the_correct_details()
        {
            CounterpartyDataChecker.CompareContractWithSavedEntity(returnedCounterparty);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_counterparty_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Counterparty counterparty;
        private static RWEST.Nexus.MDM.Contracts.Counterparty returnedCounterparty;
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
            counterparty = CounterpartyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Counterparty"] + string.Format("{0}?as-of={1}",
                    counterparty.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedCounterparty = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Counterparty>();
        }

        [TestMethod]
        public void should_return_the_counterparty_with_the_correct_details()
        {
            CounterpartyDataChecker.CompareContractWithSavedEntity(returnedCounterparty);
        }
    }
}
namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_broker_and_they_exist : IntegrationTestBase
    {
        private static MDM.Broker broker;

        private static RWEST.Nexus.MDM.Contracts.Broker returnedBroker;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            broker = BrokerData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Broker"] + 
                broker.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBroker = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Broker>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_broker_with_the_correct_details()
        {
            BrokerDataChecker.CompareContractWithSavedEntity(returnedBroker);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_broker_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Broker broker;
        private static RWEST.Nexus.MDM.Contracts.Broker returnedBroker;
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
            broker = BrokerData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Broker"] + string.Format("{0}?as-of={1}",
                    broker.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedBroker = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Broker>();
        }

        [TestMethod]
        public void should_return_the_broker_with_the_correct_details()
        {
            BrokerDataChecker.CompareContractWithSavedEntity(returnedBroker);
        }
    }
}
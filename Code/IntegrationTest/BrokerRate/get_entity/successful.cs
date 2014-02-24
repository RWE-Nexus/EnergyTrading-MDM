namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_for_a_brokerrate_and_they_exist : IntegrationTestBase
    {
        private static MDM.BrokerRate brokerrate;

        private static RWEST.Nexus.MDM.Contracts.BrokerRate returnedBrokerRate;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            brokerrate = BrokerRateData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["BrokerRate"] + 
                brokerrate.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBrokerRate = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.BrokerRate>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_brokerrate_with_the_correct_details()
        {
            BrokerRateDataChecker.CompareContractWithSavedEntity(returnedBrokerRate);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_brokerrate_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.BrokerRate brokerrate;
        private static RWEST.Nexus.MDM.Contracts.BrokerRate returnedBrokerRate;
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
            brokerrate = BrokerRateData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["BrokerRate"] + string.Format("{0}?as-of={1}",
                    brokerrate.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedBrokerRate = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.BrokerRate>();
        }

        [TestMethod]
        public void should_return_the_brokerrate_with_the_correct_details()
        {
            BrokerRateDataChecker.CompareContractWithSavedEntity(returnedBrokerRate);
        }
    }

    [TestClass]
    public class when_a_list_request_is_made_for_a_brokerrate_and_they_exist : IntegrationTestBase
    {
        private static MDM.BrokerRate brokerrate;

        private static IList<RWEST.Nexus.MDM.Contracts.BrokerRate> returnedBrokerRates;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            brokerrate = BrokerRateData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["BrokerRate"] + string.Format("{0}/list",
                    brokerrate.Id.ToString())))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBrokerRates = response.Content.ReadAsDataContract<IList<BrokerRate>>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_brokerrate_with_the_correct_details()
        {
            foreach (var brokerrate in returnedBrokerRates)
            {
                BrokerRateDataChecker.CompareContractWithSavedEntity(brokerrate);
            }
        }
    }
}
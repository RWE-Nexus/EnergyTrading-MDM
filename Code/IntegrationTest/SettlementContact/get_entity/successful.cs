namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_settlementcontact_and_they_exist : IntegrationTestBase
    {
        private static MDM.SettlementContact settlementcontact;

        private static RWEST.Nexus.MDM.Contracts.SettlementContact returnedSettlementContact;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            settlementcontact = SettlementContactData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["SettlementContact"] + 
                settlementcontact.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedSettlementContact = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.SettlementContact>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_settlementcontact_with_the_correct_details()
        {
            SettlementContactDataChecker.CompareContractWithSavedEntity(returnedSettlementContact);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_settlementcontact_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.SettlementContact settlementcontact;
        private static RWEST.Nexus.MDM.Contracts.SettlementContact returnedSettlementContact;
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
            settlementcontact = SettlementContactData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["SettlementContact"] + string.Format("{0}?as-of={1}",
                    settlementcontact.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedSettlementContact = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.SettlementContact>();
        }

        [TestMethod]
        public void should_return_the_settlementcontact_with_the_correct_details()
        {
            SettlementContactDataChecker.CompareContractWithSavedEntity(returnedSettlementContact);
        }
    }
}
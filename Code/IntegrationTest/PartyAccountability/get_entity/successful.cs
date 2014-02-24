namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_partyaccountability_and_they_exist : IntegrationTestBase
    {
        private static MDM.PartyAccountability partyaccountability;

        private static RWEST.Nexus.MDM.Contracts.PartyAccountability returnedPartyAccountability;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            partyaccountability = PartyAccountabilityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["PartyAccountability"] + 
                partyaccountability.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedPartyAccountability = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.PartyAccountability>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_partyaccountability_with_the_correct_details()
        {
            PartyAccountabilityDataChecker.CompareContractWithSavedEntity(returnedPartyAccountability);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_partyaccountability_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.PartyAccountability partyaccountability;
        private static RWEST.Nexus.MDM.Contracts.PartyAccountability returnedPartyAccountability;
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
            partyaccountability = PartyAccountabilityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["PartyAccountability"] + string.Format("{0}?as-of={1}",
                    partyaccountability.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedPartyAccountability = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.PartyAccountability>();
        }

        [TestMethod]
        public void should_return_the_partyaccountability_with_the_correct_details()
        {
            PartyAccountabilityDataChecker.CompareContractWithSavedEntity(returnedPartyAccountability);
        }
    }
}
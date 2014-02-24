namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_party_and_they_exist : IntegrationTestBase
    {
        private static MDM.Party party;

        private static RWEST.Nexus.MDM.Contracts.Party returnedParty;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            party = Script.PartyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Party"] + 
                party.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedParty = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Party>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_party_with_the_correct_details()
        {
            Script.PartyDataChecker.CompareContractWithSavedEntity(returnedParty);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_party_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Party party;
        private static RWEST.Nexus.MDM.Contracts.Party returnedParty;
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
            party = Script.PartyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Party"] + string.Format("{0}?as-of={1}",
                    party.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedParty = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Party>();
        }

        [TestMethod]
        public void should_return_the_party_with_the_correct_details()
        {
            Script.PartyDataChecker.CompareContractWithSavedEntity(returnedParty);
        }
    }
}
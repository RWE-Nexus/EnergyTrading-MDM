namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_partyoverride_and_they_exist : IntegrationTestBase
    {
        private static MDM.PartyOverride partyoverride;

        private static RWEST.Nexus.MDM.Contracts.PartyOverride returnedPartyOverride;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            partyoverride = PartyOverrideData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["PartyOverride"] + 
                partyoverride.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedPartyOverride = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.PartyOverride>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_partyoverride_with_the_correct_details()
        {
            PartyOverrideDataChecker.CompareContractWithSavedEntity(returnedPartyOverride);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_partyoverride_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.PartyOverride partyoverride;
        private static RWEST.Nexus.MDM.Contracts.PartyOverride returnedPartyOverride;
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
            partyoverride = PartyOverrideData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["PartyOverride"] + string.Format("{0}?as-of={1}",
                    partyoverride.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedPartyOverride = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.PartyOverride>();
        }

        [TestMethod]
        public void should_return_the_partyoverride_with_the_correct_details()
        {
            PartyOverrideDataChecker.CompareContractWithSavedEntity(returnedPartyOverride);
        }
    }
}
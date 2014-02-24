namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_vessel_and_they_exist : IntegrationTestBase
    {
        private static MDM.Vessel vessel;

        private static RWEST.Nexus.MDM.Contracts.Vessel returnedVessel;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            vessel = VesselData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Vessel"] + 
                vessel.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedVessel = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Vessel>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_vessel_with_the_correct_details()
        {
            VesselDataChecker.CompareContractWithSavedEntity(returnedVessel);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_vessel_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Vessel vessel;
        private static RWEST.Nexus.MDM.Contracts.Vessel returnedVessel;
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
            vessel = VesselData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Vessel"] + string.Format("{0}?as-of={1}",
                    vessel.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedVessel = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Vessel>();
        }

        [TestMethod]
        public void should_return_the_vessel_with_the_correct_details()
        {
            VesselDataChecker.CompareContractWithSavedEntity(returnedVessel);
        }
    }
}
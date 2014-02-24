namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_instrumenttypeoverride_and_they_exist : IntegrationTestBase
    {
        private static MDM.InstrumentTypeOverride instrumenttypeoverride;

        private static RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride returnedInstrumentTypeOverride;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            instrumenttypeoverride = InstrumentTypeOverrideData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["InstrumentTypeOverride"] + 
                instrumenttypeoverride.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedInstrumentTypeOverride = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_instrumenttypeoverride_with_the_correct_details()
        {
            InstrumentTypeOverrideDataChecker.CompareContractWithSavedEntity(returnedInstrumentTypeOverride);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_instrumenttypeoverride_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.InstrumentTypeOverride instrumenttypeoverride;
        private static RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride returnedInstrumentTypeOverride;
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
            instrumenttypeoverride = InstrumentTypeOverrideData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["InstrumentTypeOverride"] + string.Format("{0}?as-of={1}",
                    instrumenttypeoverride.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedInstrumentTypeOverride = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.InstrumentTypeOverride>();
        }

        [TestMethod]
        public void should_return_the_instrumenttypeoverride_with_the_correct_details()
        {
            InstrumentTypeOverrideDataChecker.CompareContractWithSavedEntity(returnedInstrumentTypeOverride);
        }
    }
}
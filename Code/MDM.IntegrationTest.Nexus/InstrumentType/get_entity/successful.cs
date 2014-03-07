namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_instrumenttype_and_they_exist : IntegrationTestBase
    {
        private static MDM.InstrumentType instrumenttype;

        private static RWEST.Nexus.MDM.Contracts.InstrumentType returnedInstrumentType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            instrumenttype = Script.InstrumentTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["InstrumentType"] + 
                instrumenttype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedInstrumentType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.InstrumentType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_instrumenttype_with_the_correct_details()
        {
            Script.InstrumentTypeDataChecker.CompareContractWithSavedEntity(returnedInstrumentType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_instrumenttype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.InstrumentType instrumenttype;
        private static RWEST.Nexus.MDM.Contracts.InstrumentType returnedInstrumentType;
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
            instrumenttype = Script.InstrumentTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["InstrumentType"] + string.Format("{0}?as-of={1}",
                    instrumenttype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedInstrumentType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.InstrumentType>();
        }

        [TestMethod]
        public void should_return_the_instrumenttype_with_the_correct_details()
        {
            Script.InstrumentTypeDataChecker.CompareContractWithSavedEntity(returnedInstrumentType);
        }
    }
}
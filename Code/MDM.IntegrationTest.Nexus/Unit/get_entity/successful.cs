namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_unit_and_they_exist : IntegrationTestBase
    {
        private static MDM.Unit unit;

        private static RWEST.Nexus.MDM.Contracts.Unit returnedUnit;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            unit = UnitData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Unit"] + 
                unit.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedUnit = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Unit>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_unit_with_the_correct_details()
        {
            UnitDataChecker.CompareContractWithSavedEntity(returnedUnit);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_unit_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Unit unit;
        private static RWEST.Nexus.MDM.Contracts.Unit returnedUnit;
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
            unit = UnitData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Unit"] + string.Format("{0}?as-of={1}",
                    unit.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedUnit = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Unit>();
        }

        [TestMethod]
        public void should_return_the_unit_with_the_correct_details()
        {
            UnitDataChecker.CompareContractWithSavedEntity(returnedUnit);
        }
    }
}
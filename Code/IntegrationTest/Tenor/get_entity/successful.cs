namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_tenor_and_they_exist : IntegrationTestBase
    {
        private static MDM.Tenor tenor;

        private static RWEST.Nexus.MDM.Contracts.Tenor returnedTenor;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            tenor = TenorData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Tenor"] + 
                tenor.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedTenor = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Tenor>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_tenor_with_the_correct_details()
        {
            TenorDataChecker.CompareContractWithSavedEntity(returnedTenor);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_tenor_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Tenor tenor;
        private static RWEST.Nexus.MDM.Contracts.Tenor returnedTenor;
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
            tenor = TenorData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Tenor"] + string.Format("{0}?as-of={1}",
                    tenor.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedTenor = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Tenor>();
        }

        [TestMethod]
        public void should_return_the_tenor_with_the_correct_details()
        {
            TenorDataChecker.CompareContractWithSavedEntity(returnedTenor);
        }
    }
}
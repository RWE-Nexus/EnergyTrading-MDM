namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_tenortype_and_they_exist : IntegrationTestBase
    {
        private static MDM.TenorType tenortype;

        private static RWEST.Nexus.MDM.Contracts.TenorType returnedTenorType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            tenortype = TenorTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["TenorType"] + 
                tenortype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedTenorType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.TenorType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_tenortype_with_the_correct_details()
        {
            TenorTypeDataChecker.CompareContractWithSavedEntity(returnedTenorType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_tenortype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.TenorType tenortype;
        private static RWEST.Nexus.MDM.Contracts.TenorType returnedTenorType;
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
            tenortype = TenorTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["TenorType"] + string.Format("{0}?as-of={1}",
                    tenortype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedTenorType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.TenorType>();
        }

        [TestMethod]
        public void should_return_the_tenortype_with_the_correct_details()
        {
            TenorTypeDataChecker.CompareContractWithSavedEntity(returnedTenorType);
        }
    }
}
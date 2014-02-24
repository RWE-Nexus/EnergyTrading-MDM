namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_feetype_and_they_exist : IntegrationTestBase
    {
        private static MDM.FeeType feetype;

        private static RWEST.Nexus.MDM.Contracts.FeeType returnedFeeType;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            feetype = Script.FeeTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["FeeType"] +
                feetype.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedFeeType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.FeeType>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_instrumenttype_with_the_correct_details()
        {
            Script.FeeTypeDataChecker.CompareContractWithSavedEntity(returnedFeeType);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_feetype_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.FeeType feetype;
        private static RWEST.Nexus.MDM.Contracts.FeeType returnedFeeType;
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
            feetype = Script.FeeTypeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["FeeType"] + string.Format("{0}?as-of={1}",
                    feetype.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedFeeType = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.FeeType>();
        }

        [TestMethod]
        public void should_return_the_instrumenttype_with_the_correct_details()
        {
            Script.FeeTypeDataChecker.CompareContractWithSavedEntity(returnedFeeType);
        }
    }
}
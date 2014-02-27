namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_curve_and_they_exist : IntegrationTestBase
    {
        private static MDM.Curve curve;

        private static Contracts.Curve returnedCurve;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            curve = CurveData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Curve"] + 
                curve.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCurve = response.Content.ReadAsDataContract<Contracts.Curve>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_curve_with_the_correct_details()
        {
            CurveDataChecker.CompareContractWithSavedEntity(returnedCurve);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_curve_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Curve curve;
        private static Contracts.Curve returnedCurve;
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
            curve = CurveData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Curve"] + string.Format("{0}?as-of={1}",
                    curve.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedCurve = response.Content.ReadAsDataContract<Contracts.Curve>();
        }

        [TestMethod]
        public void should_return_the_curve_with_the_correct_details()
        {
            CurveDataChecker.CompareContractWithSavedEntity(returnedCurve);
        }
    }
}
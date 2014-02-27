namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    using Curve = RWEST.Nexus.MDM.Curve;

    [TestClass]
    public class when_a_request_is_made_for_an_individual_mapping_for_a_curve_and_the_mapping_doesnt_exist : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            var entity = CurveData.CreateBasicEntityWithOneMapping();

            client = new HttpClient(ServiceUrl["Curve"] + string.Format("{0}/mapping/{1}", entity.Id, int.MaxValue));

            response = client.Get();
        }

        [TestMethod]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Mapping", fault.Reason);
            Assert.AreEqual(String.Format("Mapping identified by '{0}' not found", int.MaxValue), fault.Message);
            Assert.IsNull(fault.AsOfDate);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_not_found()
        {
            response.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
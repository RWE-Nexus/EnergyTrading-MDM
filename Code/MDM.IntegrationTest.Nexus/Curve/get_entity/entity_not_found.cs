namespace RWEST.Nexus.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_for_a_curve_and_they_dont_exist : IntegrationTestBase
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
            client = new HttpClient(ServiceUrl["Curve"] +
                int.MaxValue);
            response = client.Get();
        }

        [TestMethod]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Curve", fault.Reason);
            Assert.AreEqual("Curve identified by '" + int.MaxValue + "' not found", fault.Message);
            Assert.AreEqual(int.MaxValue.ToString(), fault.Identifier);
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

    [TestClass]
    public class when_a_request_is_made_for_a_curve_as_of_a_date_and_they_dont_exist : IntegrationTestBase
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
            client = new HttpClient(ServiceUrl["Curve"] + string.Format("{0}?as-of=2010-03-16T11:21:23Z", int.MaxValue));
            response = client.Get();
        }

        [TestMethod]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            var date = new DateTime(2010, 03, 16, 11, 21, 23);
            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Curve", fault.Reason);
            Assert.AreEqual("Curve identified by '" + int.MaxValue + "' not found at the given date '" + date + "'", fault.Message);
            Assert.AreEqual(int.MaxValue.ToString(), fault.Identifier);
            Assert.AreEqual(date, fault.AsOfDate);
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

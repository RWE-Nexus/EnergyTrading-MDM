namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Mdm.Contracts;

    [TestFixture]
    public class when_a_request_is_made_for_a_sourcesystem_and_they_dont_exist : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["SourceSystem"] +
                int.MaxValue);
            response = client.Get();
        }

        [Test]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown SourceSystem", fault.Reason);
            Assert.AreEqual("SourceSystem identified by '" + int.MaxValue + "' not found", fault.Message);
            Assert.AreEqual(int.MaxValue.ToString(), fault.Identifier);
            Assert.IsNull(fault.AsOfDate);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_status_not_found()
        {
            response.StatusCode = HttpStatusCode.NotFound;
        }
    }

    [TestFixture]
    public class when_a_request_is_made_for_a_sourcesystem_as_of_a_date_and_they_dont_exist : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["SourceSystem"] + string.Format("{0}?as-of=2010-03-16T11:21:23Z", int.MaxValue));
            response = client.Get();
        }

        [Test]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            var date = new DateTime(2010, 03, 16, 11, 21, 23);
            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown SourceSystem", fault.Reason);
            Assert.AreEqual("SourceSystem identified by '" + int.MaxValue + "' not found at the given date '" + date + "'", fault.Message);
            Assert.AreEqual(int.MaxValue.ToString(), fault.Identifier);
            Assert.AreEqual(date, fault.AsOfDate);
        }

        [Test]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [Test]
        public void should_return_status_not_found()
        {
            response.StatusCode = HttpStatusCode.NotFound;
        }
    }
}

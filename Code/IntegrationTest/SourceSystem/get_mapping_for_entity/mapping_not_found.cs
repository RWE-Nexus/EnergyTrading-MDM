namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Mdm.Contracts;

    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    [TestFixture]
    public class when_a_request_is_made_for_an_individual_mapping_for_a_sourcesystem_and_the_mapping_doesnt_exist : IntegrationTestBase
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
            var entity = Script.SourceSystemData.CreateBasicEntityWithOneMapping();

            client = new HttpClient(ServiceUrl["SourceSystem"] + string.Format("{0}/mapping/{1}", entity.Id, int.MaxValue));

            response = client.Get();
        }

        [Test]
        public void should_return_nexus_failure_with_correct_information()
        {
            Fault fault = null;
            try { fault = response.Content.ReadAsDataContract<Fault>(); } catch { }

            Assert.IsNotNull(fault);
            Assert.AreEqual("Unknown Mapping", fault.Reason);
            Assert.AreEqual(String.Format("Mapping identified by '{0}' not found", int.MaxValue), fault.Message);
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
}
namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_sourcesystem_entity_and_the_etag_is_not_current : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static ulong startVersion;
        private static MDM.SourceSystem entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.SourceSystemData.CreateBasicEntity();

            content = HttpContentExtensions.CreateDataContract(new RWEST.Nexus.MDM.Contracts.SourceSystem());
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", long.MaxValue.ToString());
            response = client.Post(ServiceUrl["SourceSystem"] + entity.Id, content);
        }

        [TestMethod]
        public void should_not_update_the_sourcesystem_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_no_content_status_code()
        {
            Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            return new DbSetRepository<MDM.SourceSystem>(new NexusMappingContext()).FindOne(entity.Id).Version;
        }
    }
}


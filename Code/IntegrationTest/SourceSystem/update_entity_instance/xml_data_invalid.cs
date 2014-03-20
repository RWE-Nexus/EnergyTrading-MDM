namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestFixture]
    public class when_a_request_is_made_to_update_a_sourcesystem_entity_and_the_xml_does_not_satisfy_the_schema_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.SourceSystem entity;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            var notASourceSystem = new Mapping();
            content = HttpContentExtensions.CreateDataContract(notASourceSystem);
        }

        protected static void Because_of()
        {
            entity = Script.SourceSystemData.CreateBasicEntity();
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["SourceSystem"] + entity.Id, content);
        }

        [Test]
        public void should_not_update_the_sourcesystem_in_the_database()
        {
            Assert.AreEqual(entity.Version, CurrentEntityVersion());
        }

        [Test]
        public void should_return_a_bad_request_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            return new DbSetRepository(new DbContextProvider(() => new MappingContext())).FindOne<MDM.SourceSystem>(entity.Id).Version;
        }
    }
}


namespace EnergyTrading.MDM.Test
{
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_legalentity_entity_and_the_xml_does_not_satisfy_the_schema_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.LegalEntity entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            var notALegalEntity = new Mapping();
            content = HttpContentExtensions.CreateDataContract(notALegalEntity);
        }

        protected static void Because_of()
        {
            entity = LegalEntityData.CreateBasicEntity();
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            response = client.Post(ServiceUrl["LegalEntity"] + entity.Id, content);
        }

        [TestMethod]
        public void should_not_update_the_legalentity_in_the_database()
        {
            Assert.AreEqual(entity.Version, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_bad_request_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            return new DbSetRepository<MDM.LegalEntity>(new MappingContext()).FindOne(entity.Id).Version;
        }
    }
}


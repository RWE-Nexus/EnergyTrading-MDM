namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using EnergyTrading.MDM.Extensions;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_bookdefault_mapping_and_the_xml_does_not_satisfy_the_schema_ : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpContent content;
        private static HttpClient client;
        private static ulong startVersion;
        private static MDM.BookDefault entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = BookDefaultData.CreateBasicEntityWithOneMapping();
            client = new HttpClient();
            var notAMapping = new MDM.BookDefault();
            content = HttpContentExtensions.CreateDataContract(notAMapping);
            startVersion = CurrentEntityVersion();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Mappings[0].Version.ToUnsignedLongVersion().ToString());

            response = client.Post(ServiceUrl["BookDefault"] + string.Format("{0}/Mapping/{1}", entity.Id, 
                int.MaxValue), content);
        }

        [TestMethod]
        public void should_not_update_the_bookdefault_mapping_in_the_database()
        {
            Assert.AreEqual(startVersion, CurrentEntityVersion());
        }

        [TestMethod]
        public void should_return_a_bad_request_status_code()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static ulong CurrentEntityVersion()
        {
            byte[] b = new DbSetRepository<MDM.BookDefaultMapping>(new NexusMappingContext()).FindOne(entity.Mappings[0].Id).Version;
            return b.ToUnsignedLongVersion();
        }
    }
}



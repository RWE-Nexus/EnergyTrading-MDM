namespace EnergyTrading.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass, Ignore]
    public class when_a_request_is_made_to_delete_a_sourcesystem_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;
        private static MDM.SourceSystem sourcesystem;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            sourcesystem = Script.SourceSystemData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient();
            var uri = ServiceUrl["SourceSystem"] + sourcesystem.Id + "/Mapping/" + sourcesystem.Mappings[0].Id;
            response = client.Delete(uri);
        }

        [TestMethod]
        public void should_delete_the_mapping()
        {
            var dbSourceSystem =
                new DbSetRepository<MDM.SourceSystem>(new MappingContext()).FindOne(sourcesystem.Id);

            Assert.IsTrue(dbSourceSystem.Mappings.Where(mapping => mapping.Id == sourcesystem.Mappings[0].Id).Count() == 0);
        }

        [TestMethod]
        public void should_leave_other_mappings_untouched()
        {
            var dbSourceSystem =
                new DbSetRepository<MDM.SourceSystem>(new MappingContext()).FindOne(sourcesystem.Id);

            Assert.AreEqual(1, dbSourceSystem.Mappings.Count);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }
    }

}
    
namespace RWEST.Nexus.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    using Curve = RWEST.Nexus.MDM.Curve;

    [TestClass]
    public class when_a_request_is_made_for_an_individual_mapping_for_a_curve : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MappingResponse mappingResponse;
        private static HttpClient client;
        private static Curve entity;
        private static CurveMapping mapping;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Because_of();
        }

        protected static void Because_of()
        {
            entity = CurveData.CreateBasicEntityWithOneMapping();
            mapping = entity.Mappings[0];
            client = new HttpClient(ServiceUrl["Curve"] + string.Format("{0}/mapping/{1}", entity.Id, mapping.Id));

            response = client.Get();
            mappingResponse = response.Content.ReadAsDataContract<Contracts.MappingResponse>();
        }

        [TestMethod]
        public void should_return_the_correct_vesrion_of_the_mapping()
        {
            Assert.AreEqual(mapping.Validity.Start, mappingResponse.Mappings[0].StartDate);
            Assert.AreEqual(mapping.Validity.Finish, mappingResponse.Mappings[0].EndDate);
            Assert.AreEqual(mapping.System.Name, mappingResponse.Mappings[0].SystemName);
            Assert.IsFalse(mappingResponse.Mappings[0].IsNexusId);
            Assert.AreEqual(mapping.Id, mappingResponse.Mappings[0].MappingId);
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }

        [TestMethod]
        public void should_return_only_one_mapping()
        {
            Assert.AreEqual(1, mappingResponse.Mappings.Count);
        }
    }
}
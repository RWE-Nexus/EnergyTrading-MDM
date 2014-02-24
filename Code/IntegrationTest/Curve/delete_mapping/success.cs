namespace RWEST.Nexus.MDM.Test
{
    using System.Configuration;
    using System.Linq;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.Data.EntityFramework;
    using RWEST.Nexus.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_delete_a_curve_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static HttpClient client;
        private static MDM.Curve curve;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            curve = CurveData.CreateEntityWithTwoDetailsAndTwoMappings();
        }

        protected static void Because_of()
        {
            client = new HttpClient();
            var uri = ServiceUrl["Curve"] + curve.Id + "/Mapping/" + curve.Mappings[0].Id;
            response = client.Delete(uri);
        }

        [TestMethod]
        public void should_delete_the_mapping()
        {
            var dbCurve =
                new DbSetRepository<MDM.Curve>(new MappingContext()).FindOne(curve.Id);

            Assert.IsTrue(dbCurve.Mappings.Where(mapping => mapping.Id == curve.Mappings[0].Id).Count() == 0);
        }

        [TestMethod]
        public void should_leave_other_mappings_untouched()
        {
            var dbCurve =
                new DbSetRepository<MDM.Curve>(new MappingContext()).FindOne(curve.Id);

            Assert.AreEqual(1, dbCurve.Mappings.Count);
        }

        [TestMethod]
        public void should_return_status_ok()
        {
            response.StatusCode = HttpStatusCode.OK;
        }
    }

}
    
namespace RWEST.Nexus.MDM.Test.ReferenceData
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RWEST.Nexus.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using RWEST.Nexus.MDM.Data.EF.Configuration;
    using ReferenceData = RWEST.Nexus.MDM.ReferenceData;

    [TestClass]
    public class when_a_request_is_made_for_reference_data_for_a_specific_type : IntegrationTestBase
    {
        private static HttpContent content;
        private static HttpClient client;
        private static ReferenceDataList response;
        private static ReferenceData refData;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            refData = new ReferenceData() { Key = "LocationType", Value = Guid.NewGuid().ToString() };
            var repository = new DbSetRepository<MDM.ReferenceData>(new MappingContext());

            foreach (var rd in repository.Queryable())
            {
                repository.Delete(rd);
            }

            repository.Add(refData);
            repository.Flush();

            client = new HttpClient();
        }

        protected static void Because_of()
        {
            var getResponse = client.Get(ServiceUrl["ReferenceData"] + string.Format("/list/{0}", refData.Key));
            response = getResponse.Content.ReadAsDataContract<Contracts.ReferenceDataList>();
        }

        [TestMethod]
        public void should_return_the_matching_reference_data()
        {
            Assert.AreEqual(response[0].Value, refData.Value);
        }
    }
}
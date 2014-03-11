namespace EnergyTrading.MDM.Test
{
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_party_is_created_and_the_latest_detail_is_updated : IntegrationTestBase
    {
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.Party entity;
        private static RWEST.Nexus.MDM.Contracts.Party updatedContract;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.PartyData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Party"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Party>();
            updatedContract.Details.Name = "Bob";
            updatedContract.Identifiers.Remove(updatedContract.Identifiers.Where(id => id.IsNexusId).First());
            content = HttpContentExtensions.CreateDataContract(updatedContract);
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            client.Post(ServiceUrl["Party"] + entity.Id, content);
        }

        [TestMethod]
        public void should_update_the_latest_party_details()
        {
            var party = new DbSetRepository<MDM.Party>(new NexusMappingContext()).FindOne(entity.Id);
            Assert.AreEqual(party.LatestDetails.Name, "Bob");
        }

        [TestMethod]
        public void should_not_create_a_new_detail()
        {
            var party = new DbSetRepository<MDM.Party>(new NexusMappingContext()).FindOne(entity.Id);
            Assert.AreEqual(party.Details.Count, 1);
        }
    }
}


namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;

    /// <summary>
    /// There seems to be an issue when updating mappings via the ui that they are failing on version conflict
    /// </summary>
    [TestClass]
    public class when_a_mapping_is_requested_and_then_updated_the_etag_doesnt_fail : IntegrationTestBase
    {
        private static HttpClient client;
        private static MDM.Person entity;
        private static RWEST.Nexus.MDM.Contracts.Person person;
        private static DateTime newEndDate;
        private static HttpResponseMessage mappingUpdateResponse;
        private static NexusId mappingId;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.PersonData.CreateBasicEntityWithOneMapping();
            var getResponse = client.Get(ServiceUrl["Person"] + entity.Id);
            person = getResponse.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Person>();
            mappingId = person.Identifiers.Where(x => !x.IsNexusId).First();

            var mappingGetResponse = client.Get(ServiceUrl["Person"] +  person.NexusId() + "/mapping/" + mappingId.MappingId);
            var mapping_etag = mappingGetResponse.Headers.ETag; 
            var mappingFromService = mappingGetResponse.Content.ReadAsDataContract<MappingResponse>();

            NexusId postMapping = mappingFromService.Mappings[0];
            newEndDate = mappingFromService.Mappings[0].EndDate.Value.AddDays(1);
            postMapping.EndDate = newEndDate;
            var content = HttpContentExtensions.CreateDataContract(postMapping);
            client.DefaultHeaders.Add("If-Match", mapping_etag != null ? mapping_etag.Tag : string.Empty);
            mappingUpdateResponse = client.Post(ServiceUrl["Person"] +  string.Format("{0}/Mapping/{1}", entity.Id, mappingFromService.Mappings[0].MappingId), content);
        }

        protected static void Because_of()
        {
/*
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            client.Post(ServiceUrl["Person"] + entity.Id, content);
*/
        }

        [TestMethod]
        public void should_update_the_mapping()
        {
            var mappingGetResponse = client.Get(ServiceUrl["Person"] +  person.NexusId() + "/mapping/" + mappingId.MappingId);
            Assert.AreEqual(
                newEndDate, mappingGetResponse.Content.ReadAsDataContract<MappingResponse>().Mappings[0].EndDate);
        }
    }

    public static class IMdmEntityExtensions
    {
        public static int? NexusId(this IMdmEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return
                entity.Identifiers.Where(id => id.IsNexusId).Select(
                    nexusId => nexusId.Identifier == null ? null : new int?(int.Parse(nexusId.Identifier))).
                    FirstOrDefault();
        }
    }
}
namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    [TestClass]
    public class when_a_request_is_made_to_update_a_commodityinstrumenttype_mapping : IntegrationTestBase
    {
        private static HttpResponseMessage response;

        private static Mapping mapping;

        private static HttpContent content;

        private static HttpClient client;

        private static CommodityInstrumentTypeMapping currentTrayportMapping;

        private static MDM.CommodityInstrumentType entity;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity = CommodityInstrumentTypeData.CreateBasicEntityWithOneMapping();
            currentTrayportMapping = entity.Mappings[0];

            mapping = new Mapping {
                
                    SystemName = currentTrayportMapping.System.Name,
                    Identifier = currentTrayportMapping.MappingValue,
                    SourceSystemOriginated = currentTrayportMapping.IsMaster,
                    DefaultReverseInd = currentTrayportMapping.IsDefault,
                    StartDate = currentTrayportMapping.Validity.Start,
                    EndDate = currentTrayportMapping.Validity.Finish.AddDays(2)
                };

            content = HttpContentExtensions.CreateDataContract(mapping);
            client = new HttpClient();
        }

        protected static void Because_of()
        {
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString(System.Globalization.CultureInfo.InvariantCulture));

            response = client.Post(ServiceUrl["CommodityInstrumentType"] +  string.Format("{0}/Mapping/{1}", entity.Id, currentTrayportMapping.Id), content);
        }

        [TestMethod]
        public void should_update_the_mapping_on_the_commodityinstrumenttype_entity()
        {
            var savedMapping = new DbSetRepository<MDM.CommodityInstrumentType>(new NexusMappingContext()).FindOne(entity.Id).Mappings[0];

            Assert.AreEqual(currentTrayportMapping.System.Name, savedMapping.System.Name);
            Assert.AreEqual(currentTrayportMapping.MappingValue, savedMapping.MappingValue);
            Assert.AreEqual(currentTrayportMapping.IsMaster, savedMapping.IsMaster);
            Assert.AreEqual(currentTrayportMapping.IsDefault, savedMapping.IsDefault);
            Assert.AreEqual(currentTrayportMapping.Validity.Start, savedMapping.Validity.Start);
            Assert.AreEqual(currentTrayportMapping.Validity.Finish.AddDays(2), savedMapping.Validity.Finish);
        }

        [TestMethod]
        public void should_return_an_created_status_code()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}


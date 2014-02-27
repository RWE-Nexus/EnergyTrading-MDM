namespace EnergyTrading.MDM.Test 
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using DateRange = EnergyTrading.DateRange;
    using Location = EnergyTrading.MDM.Location;
    using SourceSystem = EnergyTrading.MDM.SourceSystem;

    [TestClass]
    public class when_a_source_system_to_destination_system_mapping_request_is_made_for_location_and_the_destination_has_more_than_one_mapping_and_no_default : IntegrationTestBase
    {
        private static HttpResponseMessage response;
        private static MDM.Location entity;
        private static Fault mappingResponse;
        private static HttpClient client;
        private static LocationMapping trayportMapping;
        private static LocationMapping endurMapping;
        private static LocationMapping endurMapping2;
        private static SourceSystem endur;
        private static SourceSystem trayport;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_Context();
            Because_of();
        }

        private static void Establish_Context()
        {
            var repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));

            endur = new SourceSystem { Name = "Endur" + Guid.NewGuid() };
            trayport = new SourceSystem { Name = "Trayport" + Guid.NewGuid() };

            var entity = new Location { Name = Guid.NewGuid().ToString() };

            trayportMapping = new LocationMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = trayport,
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            endurMapping = new LocationMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = endur,
                    IsDefault = false,
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            endurMapping2 = new LocationMapping
                {
                    MappingValue = Guid.NewGuid().ToString(),
                    System = endur,
                    IsDefault = false,
                    Validity = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

            repository.Add(endur);
            repository.Add(trayport);
            entity.ProcessMapping(trayportMapping);
            entity.ProcessMapping(endurMapping);
            entity.ProcessMapping(endurMapping2);

            repository.Add(entity);
            repository.Flush();
        }

        protected static void Because_of()
        {
            client = new HttpClient(ServiceUrl["Location"] +
                "crossmap?source-system=" + trayport.Name + "&destination-system=" + endur.Name + "&mapping-string=" + trayportMapping.MappingValue);

            response = client.Get();

            mappingResponse = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Fault>();
        }

        [TestMethod]
        public void should_return_correct_content_type()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["restReturnType"], response.Content.ContentType);
        }

        [TestMethod]
        public void should_return_status_not_found()
        {
            response.StatusCode = HttpStatusCode.NotFound;
        }

        [TestMethod]
        public void should_return_message_stating_there_is_an_ambiguous_mapping()
        {
            Assert.AreEqual(
                "Ambiguous Mappings were found for Mapping String '" + trayportMapping.MappingValue +
                "' for Source System '" + trayport.Name + "' and Destination System '" + endur.Name + "'",
                mappingResponse.Message);
        }
    }
}

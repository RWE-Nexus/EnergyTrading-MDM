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

    using Person = EnergyTrading.MDM.Person;

    [TestClass]
    public class when_a_person_entity_is_not_valid_and_the_list_function_is_called : IntegrationTestBase
    {
        private static HttpContent content;
        private static HttpClient client;
        private static MDM.Person entity;
        private static RWEST.Nexus.MDM.Contracts.Person updatedContract;
        private static Person entity2;
        private static Person entity3;

        private static PersonList returnedPersons;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            client = new HttpClient();
            entity = Script.PersonData.CreateBasicEntity();
            entity2 = Script.PersonData.CreateBasicEntity();
            entity3 = Script.PersonData.CreateBasicEntity();
            var getResponse = client.Get(ServiceUrl["Person"] + entity.Id);

            updatedContract = getResponse.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Person>();
            updatedContract.Nexus.EndDate = DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0));

            updatedContract.Identifiers.Remove(updatedContract.Identifiers.Where(id => id.IsNexusId).First());
            content = HttpContentExtensions.CreateDataContract(updatedContract);
            client.DefaultHeaders.Add("If-Match", entity.Version.ToString());
            client.Post(ServiceUrl["Person"] + entity.Id, content);
        }

        protected static void Because_of()
        {
            using (var client2 = new HttpClient(ServiceUrl["Person"] + "list"))
            {
                using (HttpResponseMessage response = client2.Get())
                {
                    returnedPersons = response.Content.ReadAsDataContract<PersonList>();
                }
            }
        }

        [TestMethod]
        public void should_not_return_the_invalid_person()
        {
            var person = new DbSetRepository<MDM.Person>(new NexusMappingContext()).FindOne(entity.Id);
            Assert.AreEqual(0, returnedPersons.Where(person1 => person1.NexusId() == entity.Id).Count());
        }
    }
}



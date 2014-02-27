namespace EnergyTrading.MDM.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Linq;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_for_all_broker : IntegrationTestBase
    {
        private static IList<RWEST.Nexus.MDM.Contracts.Broker> returnedBrokers;

        private static MDM.Broker entity1;

        private static MDM.Broker entity2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = BrokerData.CreateBasicEntity();
            entity2 = BrokerData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Broker"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBrokers = response.Content.ReadAsDataContract<BrokerList>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_broker_with_the_correct_details()
        {
            foreach (var broker in returnedBrokers)
            {
                BrokerDataChecker.CompareContractWithSavedEntity(broker);
            }
        }

        [TestMethod]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<RWEST.Nexus.MDM.Contracts.NexusId> entityIds = returnedBrokers.Select(x => x.Identifiers.First(id => id.IsNexusId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	
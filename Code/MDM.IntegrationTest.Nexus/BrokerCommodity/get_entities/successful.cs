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
    public class when_a_request_is_made_for_all_brokercommodity : IntegrationTestBase
    {
        private static IList<RWEST.Nexus.MDM.Contracts.BrokerCommodity> returnedBrokerCommoditys;

        private static MDM.BrokerCommodity entity1;

        private static MDM.BrokerCommodity entity2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = BrokerCommodityData.CreateBasicEntity();
            entity2 = BrokerCommodityData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["BrokerCommodity"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBrokerCommoditys = response.Content.ReadAsDataContract<BrokerCommodityList>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_brokercommodity_with_the_correct_details()
        {
            foreach (var brokercommodity in returnedBrokerCommoditys)
            {
                BrokerCommodityDataChecker.CompareContractWithSavedEntity(brokercommodity);
            }
        }

        [TestMethod]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<RWEST.Nexus.MDM.Contracts.NexusId> entityIds = returnedBrokerCommoditys.Select(x => x.Identifiers.First(id => id.IsNexusId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	
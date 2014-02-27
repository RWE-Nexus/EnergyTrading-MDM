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
    public class when_a_request_is_made_for_all_sourcesystem : IntegrationTestBase
    {
        private static IList<RWEST.Nexus.MDM.Contracts.SourceSystem> returnedSourceSystems;

        private static MDM.SourceSystem entity1;

        private static MDM.SourceSystem entity2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = Script.SourceSystemData.CreateBasicEntity();
            entity2 = Script.SourceSystemData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["SourceSystem"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedSourceSystems = response.Content.ReadAsDataContract<SourceSystemList>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_sourcesystem_with_the_correct_details()
        {
            foreach (var sourcesystem in returnedSourceSystems)
            {
                Script.SourceSystemDataChecker.CompareContractWithSavedEntity(sourcesystem);
            }
        }

        [TestMethod]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<RWEST.Nexus.MDM.Contracts.NexusId> entityIds = returnedSourceSystems.Select(x => x.Identifiers.First(id => id.IsNexusId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	
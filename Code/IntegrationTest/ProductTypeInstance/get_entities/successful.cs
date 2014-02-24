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
    public class when_a_request_is_made_for_all_producttypeinstance : IntegrationTestBase
    {
        private static IList<RWEST.Nexus.MDM.Contracts.ProductTypeInstance> returnedProductTypeInstances;

        private static MDM.ProductTypeInstance entity1;

        private static MDM.ProductTypeInstance entity2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = Script.ProductTypeInstanceData.CreateBasicEntity();
            entity2 = Script.ProductTypeInstanceData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["ProductTypeInstance"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedProductTypeInstances = response.Content.ReadAsDataContract<ProductTypeInstanceList>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_producttypeinstance_with_the_correct_details()
        {
            foreach (var producttypeinstance in returnedProductTypeInstances)
            {
                Script.ProductTypeInstanceDataChecker.CompareContractWithSavedEntity(producttypeinstance);
            }
        }

        [TestMethod]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<RWEST.Nexus.MDM.Contracts.NexusId> entityIds = returnedProductTypeInstances.Select(x => x.Identifiers.First(id => id.IsNexusId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	
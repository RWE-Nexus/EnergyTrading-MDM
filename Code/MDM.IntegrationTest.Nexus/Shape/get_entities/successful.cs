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
    public class when_a_request_is_made_for_all_shape : IntegrationTestBase
    {
        private static IList<RWEST.Nexus.MDM.Contracts.Shape> returnedShapes;

        private static MDM.Shape entity1;

        private static MDM.Shape entity2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = ShapeData.CreateBasicEntity();
            entity2 = ShapeData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Shape"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedShapes = response.Content.ReadAsDataContract<ShapeList>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_shape_with_the_correct_details()
        {
            foreach (var shape in returnedShapes)
            {
                ShapeDataChecker.CompareContractWithSavedEntity(shape);
            }
        }

        [TestMethod]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<RWEST.Nexus.MDM.Contracts.NexusId> entityIds = returnedShapes.Select(x => x.Identifiers.First(id => id.IsNexusId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	
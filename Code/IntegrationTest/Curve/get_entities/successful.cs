namespace RWEST.Nexus.MDM.Test
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Linq;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_for_all_curve : IntegrationTestBase
    {
        private static IList<Contracts.Curve> returnedCurves;

        private static MDM.Curve entity1;

        private static MDM.Curve entity2;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            entity1 = CurveData.CreateBasicEntity();
            entity2 = CurveData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Curve"] + "list"))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedCurves = response.Content.ReadAsDataContract<CurveList>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_curve_with_the_correct_details()
        {
            foreach (var curve in returnedCurves)
            {
                CurveDataChecker.CompareContractWithSavedEntity(curve);
            }
        }

        [TestMethod]
        public void should_contain_the_new_entities_that_were_added()
        {
            IList<NexusId> entityIds = returnedCurves.Select(x => x.Identifiers.First(id => id.IsNexusId)).ToList();
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity1.Id.ToString()));
            Assert.IsTrue(entityIds.Any(nexusId => nexusId.Identifier == entity2.Id.ToString()));
        }
    }
}
	
	
namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_hierarchy_and_they_exist : IntegrationTestBase
    {
        private static MDM.Hierarchy hierarchy;

        private static RWEST.Nexus.MDM.Contracts.Hierarchy returnedHierarchy;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            hierarchy = HierarchyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Hierarchy"] + 
                hierarchy.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedHierarchy = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Hierarchy>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_hierarchy_with_the_correct_details()
        {
            HierarchyDataChecker.CompareContractWithSavedEntity(returnedHierarchy);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_hierarchy_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Hierarchy hierarchy;
        private static RWEST.Nexus.MDM.Contracts.Hierarchy returnedHierarchy;
        private static DateTime asof;
        private static HttpClient client;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            hierarchy = HierarchyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Hierarchy"] + string.Format("{0}?as-of={1}",
                    hierarchy.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedHierarchy = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Hierarchy>();
        }

        [TestMethod]
        public void should_return_the_hierarchy_with_the_correct_details()
        {
            HierarchyDataChecker.CompareContractWithSavedEntity(returnedHierarchy);
        }
    }
}
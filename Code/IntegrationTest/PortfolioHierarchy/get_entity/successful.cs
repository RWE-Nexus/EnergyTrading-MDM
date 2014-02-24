namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_portfoliohierarchy_and_they_exist : IntegrationTestBase
    {
        private static MDM.PortfolioHierarchy portfoliohierarchy;

        private static RWEST.Nexus.MDM.Contracts.PortfolioHierarchy returnedPortfolioHierarchy;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            portfoliohierarchy = PortfolioHierarchyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["PortfolioHierarchy"] + 
                portfoliohierarchy.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedPortfolioHierarchy = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.PortfolioHierarchy>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_portfoliohierarchy_with_the_correct_details()
        {
            PortfolioHierarchyDataChecker.CompareContractWithSavedEntity(returnedPortfolioHierarchy);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_portfoliohierarchy_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.PortfolioHierarchy portfoliohierarchy;
        private static RWEST.Nexus.MDM.Contracts.PortfolioHierarchy returnedPortfolioHierarchy;
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
            portfoliohierarchy = PortfolioHierarchyData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["PortfolioHierarchy"] + string.Format("{0}?as-of={1}",
                    portfoliohierarchy.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedPortfolioHierarchy = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.PortfolioHierarchy>();
        }

        [TestMethod]
        public void should_return_the_portfoliohierarchy_with_the_correct_details()
        {
            PortfolioHierarchyDataChecker.CompareContractWithSavedEntity(returnedPortfolioHierarchy);
        }
    }
}
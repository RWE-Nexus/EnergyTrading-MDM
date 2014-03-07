namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_portfolio_and_they_exist : IntegrationTestBase
    {
        private static MDM.Portfolio portfolio;

        private static RWEST.Nexus.MDM.Contracts.Portfolio returnedPortfolio;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            portfolio = PortfolioData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Portfolio"] + 
                portfolio.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedPortfolio = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Portfolio>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_portfolio_with_the_correct_details()
        {
            PortfolioDataChecker.CompareContractWithSavedEntity(returnedPortfolio);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_portfolio_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Portfolio portfolio;
        private static RWEST.Nexus.MDM.Contracts.Portfolio returnedPortfolio;
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
            portfolio = PortfolioData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Portfolio"] + string.Format("{0}?as-of={1}",
                    portfolio.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedPortfolio = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Portfolio>();
        }

        [TestMethod]
        public void should_return_the_portfolio_with_the_correct_details()
        {
            PortfolioDataChecker.CompareContractWithSavedEntity(returnedPortfolio);
        }
    }
}
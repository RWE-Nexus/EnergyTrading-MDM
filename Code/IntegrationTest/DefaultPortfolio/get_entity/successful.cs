namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class when_a_request_is_made_for_a_bookdefault_and_they_exist : IntegrationTestBase
    {
        private static MDM.BookDefault defaultportfolio;

        private static RWEST.Nexus.MDM.Contracts.BookDefault returnedDefaultPortfolio;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            defaultportfolio = BookDefaultData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["BookDefault"] + 
                defaultportfolio.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedDefaultPortfolio = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.BookDefault>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_bookdefault_with_the_correct_details()
        {
            BookDefaultDataChecker.CompareContractWithSavedEntity(returnedDefaultPortfolio);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_bookdefault_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.BookDefault defaultportfolio;
        private static RWEST.Nexus.MDM.Contracts.BookDefault returnedDefaultPortfolio;
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
            defaultportfolio = BookDefaultData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["BookDefault"] + string.Format("{0}?as-of={1}",
                    defaultportfolio.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedDefaultPortfolio = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.BookDefault>();
        }

        [TestMethod]
        public void should_return_the_bookdefault_with_the_correct_details()
        {
            BookDefaultDataChecker.CompareContractWithSavedEntity(returnedDefaultPortfolio);
        }
    }

    [TestClass]
    public class when_a_list_request_is_made_for_a_bookdefault_and_they_exist : IntegrationTestBase
    {
        private static MDM.BookDefault defaultportfolio;

        private static IList<RWEST.Nexus.MDM.Contracts.BookDefault> returnedDefaultPortfolios;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            defaultportfolio = BookDefaultData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["BookDefault"] + string.Format("{0}/list",
                    defaultportfolio.Id.ToString())))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedDefaultPortfolios = response.Content.ReadAsDataContract<IList<BookDefault>>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_bookdefault_with_the_correct_details()
        {
            foreach (var defaultportfolio in returnedDefaultPortfolios)
            {
                BookDefaultDataChecker.CompareContractWithSavedEntity(defaultportfolio);
            }
        }
    }
}
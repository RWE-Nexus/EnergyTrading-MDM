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
    public class when_a_request_is_made_for_a_agreement_and_they_exist : IntegrationTestBase
    {
        private static MDM.Agreement agreement;

        private static RWEST.Nexus.MDM.Contracts.Agreement returnedAgreement;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            agreement = AgreementData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Agreement"] + 
                agreement.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedAgreement = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Agreement>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_agreement_with_the_correct_details()
        {
            AgreementDataChecker.CompareContractWithSavedEntity(returnedAgreement);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_agreement_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Agreement agreement;
        private static RWEST.Nexus.MDM.Contracts.Agreement returnedAgreement;
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
            agreement = AgreementData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Agreement"] + string.Format("{0}?as-of={1}",
                    agreement.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedAgreement = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Agreement>();
        }

        [TestMethod]
        public void should_return_the_agreement_with_the_correct_details()
        {
            AgreementDataChecker.CompareContractWithSavedEntity(returnedAgreement);
        }
    }

    [TestClass]
    public class when_a_list_request_is_made_for_a_agreement_and_they_exist : IntegrationTestBase
    {
        private static MDM.Agreement agreement;

        private static IList<RWEST.Nexus.MDM.Contracts.Agreement> returnedAgreements;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            agreement = AgreementData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Agreement"] + string.Format("{0}/list",
                    agreement.Id.ToString())))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedAgreements = response.Content.ReadAsDataContract<IList<Agreement>>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_agreement_with_the_correct_details()
        {
            foreach (var agreement in returnedAgreements)
            {
                AgreementDataChecker.CompareContractWithSavedEntity(agreement);
            }
        }
    }
}
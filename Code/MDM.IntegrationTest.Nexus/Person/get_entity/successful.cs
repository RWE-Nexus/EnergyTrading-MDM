namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_person_and_they_exist : IntegrationTestBase
    {
        private static MDM.Person person;

        private static RWEST.Nexus.MDM.Contracts.Person returnedPerson;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            person = Script.PersonData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Person"] + 
                person.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedPerson = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Person>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_person_with_the_correct_details()
        {
            Script.PersonDataChecker.CompareContractWithSavedEntity(returnedPerson);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_person_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Person person;
        private static RWEST.Nexus.MDM.Contracts.Person returnedPerson;
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
            person = Script.PersonData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Person"] + string.Format("{0}?as-of={1}",
                    person.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedPerson = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Person>();
        }

        [TestMethod]
        public void should_return_the_person_with_the_correct_details()
        {
            Script.PersonDataChecker.CompareContractWithSavedEntity(returnedPerson);
        }
    }
}
namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_locationrole_and_they_exist : IntegrationTestBase
    {
        private static MDM.LocationRole locationrole;

        private static RWEST.Nexus.MDM.Contracts.LocationRole returnedLocationRole;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            locationrole = Script.LocationRoleData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["LocationRole"] + 
                locationrole.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedLocationRole = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.LocationRole>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_locationrole_with_the_correct_details()
        {
            Script.LocationRoleDataChecker.CompareContractWithSavedEntity(returnedLocationRole);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_locationrole_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.LocationRole locationrole;
        private static RWEST.Nexus.MDM.Contracts.LocationRole returnedLocationRole;
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
            locationrole = Script.LocationRoleData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["LocationRole"] + string.Format("{0}?as-of={1}",
                    locationrole.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedLocationRole = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.LocationRole>();
        }

        [TestMethod]
        public void should_return_the_locationrole_with_the_correct_details()
        {
            Script.LocationRoleDataChecker.CompareContractWithSavedEntity(returnedLocationRole);
        }
    }
}
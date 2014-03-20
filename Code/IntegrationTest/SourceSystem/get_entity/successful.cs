namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using NUnit.Framework;

    [TestFixture]
    public class when_a_request_is_made_for_a_sourcesystem_and_they_exist : IntegrationTestBase
    {
        private static MDM.SourceSystem sourcesystem;

        private static EnergyTrading.Mdm.Contracts.SourceSystem returnedSourceSystem;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            sourcesystem = Script.SourceSystemData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["SourceSystem"] + 
                sourcesystem.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedSourceSystem = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.SourceSystem>();
                }
            }
        }

        [Test]
        public void should_return_the_sourcesystem_with_the_correct_details()
        {
            Script.SourceSystemDataChecker.CompareContractWithSavedEntity(returnedSourceSystem);
        }
    }

    [TestFixture]
    public class when_a_request_is_made_for_a_sourcesystem_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.SourceSystem sourcesystem;
        private static EnergyTrading.Mdm.Contracts.SourceSystem returnedSourceSystem;
        private static DateTime asof;
        private static HttpClient client;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            sourcesystem = Script.SourceSystemData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["SourceSystem"] + string.Format("{0}?as-of={1}",
                    sourcesystem.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedSourceSystem = response.Content.ReadAsDataContract<EnergyTrading.Mdm.Contracts.SourceSystem>();
        }

        [Test]
        public void should_return_the_sourcesystem_with_the_correct_details()
        {
            Script.SourceSystemDataChecker.CompareContractWithSavedEntity(returnedSourceSystem);
        }
    }
}
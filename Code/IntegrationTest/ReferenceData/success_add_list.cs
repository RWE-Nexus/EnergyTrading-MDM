namespace EnergyTrading.MDM.Test.ReferenceData
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Http;
    using NUnit.Framework;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;

    using ReferenceData = EnergyTrading.MDM.ReferenceData;

    [TestFixture]
    public class when_a_request_is_made_to_add_a_list_of_reference_data : IntegrationTestBase
    {
        private static IList<EnergyTrading.Mdm.Contracts.ReferenceData> referenceDataList;
        private static string key = "SomeRefData";
        private static HttpContent content;
        private static HttpClient client;

        private static DbSetRepository repository;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));

            foreach (var rd in repository.Queryable<MDM.ReferenceData>())
            {
                repository.Delete(rd);
            }

            referenceDataList = new List<EnergyTrading.Mdm.Contracts.ReferenceData>() { new EnergyTrading.Mdm.Contracts.ReferenceData() { Value = "test1"}, new EnergyTrading.Mdm.Contracts.ReferenceData() { Value = "test2"}};

            content = HttpContentExtensions.CreateDataContract(referenceDataList);
            client = new HttpClient();
        }

        protected static void Because_of()
        {           
            client.Post(ServiceUrl["ReferenceData"] + string.Format("/create/{0}", key), content);
        }

        [Test]
        public void should_create_each_of_the_reference_data_strings()
        {
            foreach (var rd in referenceDataList)
            {
                Assert.AreEqual(1, repository.Queryable<MDM.ReferenceData>().Where(x => x.Value == rd.Value).Count());
            }
        }
    }

    [TestFixture]
    public class when_a_request_is_made_to_add_a_list_of_reference_data_with_duplicates : IntegrationTestBase
    {
        private static IList<EnergyTrading.Mdm.Contracts.ReferenceData> referenceDataList;
        private static string key = "SomeRefData";
        private static HttpContent content;
        private static HttpClient client;

        private static DbSetRepository repository;

        [SetUp]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            repository = new DbSetRepository(new DbContextProvider(() => new MappingContext()));

            foreach (var rd in repository.Queryable<MDM.ReferenceData>())
            {
                repository.Delete(rd);
            }

            referenceDataList = new List<EnergyTrading.Mdm.Contracts.ReferenceData>() { new EnergyTrading.Mdm.Contracts.ReferenceData() { Value = "test1"}, new EnergyTrading.Mdm.Contracts.ReferenceData() { Value = "test1"}};

            content = HttpContentExtensions.CreateDataContract(referenceDataList);
            client = new HttpClient();
        }

        protected static void Because_of()
        {           
            client.Post(ServiceUrl["ReferenceData"] + string.Format("/create/{0}", key), content);
        }

        [Test]
        public void should_ignore_duplicates()
        {
            Assert.AreEqual(1, repository.Queryable<MDM.ReferenceData>().Where(x => x.Value == "test1").Count());
        }
    }
}

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
    public class when_a_request_is_made_for_a_book_and_they_exist : IntegrationTestBase
    {
        private static MDM.Book book;

        private static RWEST.Nexus.MDM.Contracts.Book returnedBook;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            book = BookData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Book"] + 
                book.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBook = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Book>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_book_with_the_correct_details()
        {
            BookDataChecker.CompareContractWithSavedEntity(returnedBook);
        }
    }

    [TestClass]
    public class when_a_request_is_made_for_a_book_as_of_a_date_and_they_exist : IntegrationTestBase
    {
        private static MDM.Book book;
        private static RWEST.Nexus.MDM.Contracts.Book returnedBook;
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
            book = BookData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            asof = Script.baseDate.AddSeconds(1);
            client =
                new HttpClient(ServiceUrl["Book"] + string.Format("{0}?as-of={1}",
                    book.Id.ToString(), asof.ToString(DateFormatString)));

            HttpResponseMessage response = client.Get();
            returnedBook = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Book>();
        }

        [TestMethod]
        public void should_return_the_book_with_the_correct_details()
        {
            BookDataChecker.CompareContractWithSavedEntity(returnedBook);
        }
    }

    [TestClass]
    public class when_a_list_request_is_made_for_a_book_and_they_exist : IntegrationTestBase
    {
        private static MDM.Book book;

        private static IList<RWEST.Nexus.MDM.Contracts.Book> returnedBooks;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            book = BookData.CreateBasicEntity();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Book"] + string.Format("{0}/list",
                    book.Id.ToString())))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedBooks = response.Content.ReadAsDataContract<IList<Book>>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_book_with_the_correct_details()
        {
            foreach (var book in returnedBooks)
            {
                BookDataChecker.CompareContractWithSavedEntity(book);
            }
        }
    }
}
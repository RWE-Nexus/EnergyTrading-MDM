namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class when_a_request_is_made_for_a_party_with_related_party_roles : IntegrationTestBase
    {
        private static MDM.Party party;

        private static RWEST.Nexus.MDM.Contracts.Party returnedParty;

        private static PartyRole partyrole;

        private static Broker broker;

        private static Counterparty counterparty;

        private static Exchange exchange;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Establish_context();
            Because_of();
        }

        protected static void Establish_context()
        {
            var repository = ObjectScript.Repository;
            party = ObjectMother.Create<Party>();
            partyrole = ObjectMother.Create<PartyRole>();
            partyrole.Party = party;
            broker = ObjectMother.Create<Broker>();
            broker.Party = party;
            counterparty = ObjectMother.Create<Counterparty>();
            counterparty.Party = party;
            exchange = ObjectMother.Create<Exchange>();
            exchange.Party = party;

            repository.Add(party);
            repository.Flush();
            repository.Add(partyrole);
            repository.Flush();
            repository.Add(counterparty);
            repository.Flush();
            repository.Add(broker);
            repository.Flush();
            repository.Add(exchange);
            repository.Flush();
        }

        protected static void Because_of()
        {
            using (var client = new HttpClient(ServiceUrl["Party"] +
                party.Id))
            {
                using (HttpResponseMessage response = client.Get())
                {
                    returnedParty = response.Content.ReadAsDataContract<RWEST.Nexus.MDM.Contracts.Party>();
                }
            }
        }

        [TestMethod]
        public void should_return_the_correct_number_of_atom_links()
        {
            Assert.AreEqual(4, returnedParty.Links.Count);
        }

        [TestMethod]
        public void should_return_the_broker_as_an_atom_link()
        {
            Assert.AreEqual(1, returnedParty.Links.Where(x => x.Type == "Broker").Count());
            var brokerLink = returnedParty.Links.Where(x => x.Type == "Broker").First();
            Assert.AreEqual("/Broker/" + broker.Id, brokerLink.Uri);
            Assert.AreEqual("get-related-broker", brokerLink.Rel);
        }

        [TestMethod]
        public void should_return_the_counterparty_as_an_atom_link()
        {
            Assert.AreEqual(1, returnedParty.Links.Where(x => x.Type == "Counterparty").Count());
            var counterpartyLink = returnedParty.Links.Where(x => x.Type == "Counterparty").First();
            Assert.AreEqual("/Counterparty/" + counterparty.Id, counterpartyLink.Uri);
            Assert.AreEqual("get-related-counterparty", counterpartyLink.Rel);
        }

        [TestMethod]
        public void should_return_the_partyrole_as_an_atom_link()
        {
            Assert.AreEqual(1, returnedParty.Links.Where(x => x.Type == "PartyRole").Count());
            var partyroleLink = returnedParty.Links.Where(x => x.Type == "PartyRole").First();
            Assert.AreEqual("/PartyRole/" + partyrole.Id, partyroleLink.Uri);
            Assert.AreEqual("get-related-partyrole", partyroleLink.Rel);
        }

        [TestMethod]
        public void should_return_the_exchange_as_an_atom_link()
        {
            Assert.AreEqual(1, returnedParty.Links.Where(x => x.Type == "Exchange").Count());
            var exchangeLink = returnedParty.Links.Where(x => x.Type == "Exchange").First();
            Assert.AreEqual("/Exchange/" + exchange.Id, exchangeLink.Uri);
            Assert.AreEqual("get-related-exchange", exchangeLink.Rel);
        }
    }
}
namespace EnergyTrading.MDM.Test.Contracts.Rules
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Rules;
    using EnergyTrading.Test;
    using Location = EnergyTrading.MDM.Location;

    [TestClass]
    public class when_a_request_is_made_to_validate_a_nexus_id_and_it_exists_in_the_database_: SpecBase<NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Market, Location, LocationMapping>>
    {
        private Market market;
        private bool isValid;
        private Mock<IRepository> repository;

        protected override NexusEntityExistsRule<Market, Location, LocationMapping> Establish_context()
        {
            this.repository = new Mock<IRepository>();
            this.repository.Setup(repository => repository.FindOne<Location>(1)).Returns(new Location() { Id = 1 });

            this.market = new RWEST.Nexus.MDM.Contracts.Market()
                {
                    Details =
                        new MarketDetails()
                            {
                                Location =
                                    new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } }
                            }
                };

            return new NexusEntityExistsRule<Market, Location, LocationMapping>(this.repository.Object, market => this.market.Details.Location, true);
        }

        protected override void Because_of()
        {
            this.isValid = this.Sut.IsValid(this.market);
        }

        [TestMethod]
        public void should_be_valid()
        {
            Assert.IsTrue(isValid); 
        }

        [TestMethod]
        public void should_not_have_an_invalid_message()
        {
            Assert.AreEqual(null, this.Sut.Message);
        }
    }

    [TestClass]
    public class when_a_request_is_made_to_validate_a_nexus_id_and_it_doesnt_exists_in_the_database_ : SpecBase<NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Market, Location, LocationMapping>>
    {
        private Market market;
        private bool isValid;
        private Mock<IRepository> repository;

        protected override NexusEntityExistsRule<Market, Location, LocationMapping> Establish_context()
        {
            this.repository = new Mock<IRepository>();
            this.repository.Setup(repository => repository.FindOne<Location>(1)).Returns(() => null);

            this.market = new RWEST.Nexus.MDM.Contracts.Market()
                {
                    Details =
                        new MarketDetails()
                            {
                                Location =
                                    new EntityId() { Identifier = new NexusId() { IsNexusId = true, Identifier = "1" } }
                            }
                };

            return new NexusEntityExistsRule<Market, Location, LocationMapping>(this.repository.Object, market => this.market.Details.Location, true);
        }

        protected override void Because_of()
        {
            this.isValid = this.Sut.IsValid(this.market);
        }

        [TestMethod]
        public void should_not_be_valid()
        {
            Assert.IsTrue(!isValid); 
        }

        [TestMethod]
        public void should_have_the_correct_message()
        {
            Assert.AreEqual("Market does not have a valid Location with id '1'", this.Sut.Message);
        }
    }

    [TestClass]
    public class when_a_request_is_made_to_validate_a_null_nexus_id_that_is_required : SpecBase<NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Market, Location, LocationMapping>>
    {
        private Market market;
        private bool isValid;
        private Mock<IRepository> repository;

        protected override NexusEntityExistsRule<Market, Location, LocationMapping> Establish_context()
        {
            this.repository = new Mock<IRepository>();
            this.repository.Setup(repository => repository.FindOne<Location>(1)).Returns(new Location() { Id = 1 });

            this.market = new RWEST.Nexus.MDM.Contracts.Market()
                {
                    Details =
                        new MarketDetails()
                            {
                                Location = null
                            }
                };

            return new NexusEntityExistsRule<Market, Location, LocationMapping>(this.repository.Object, market => this.market.Details.Location, true);
        }

        protected override void Because_of()
        {
            this.isValid = this.Sut.IsValid(this.market);
        }

        [TestMethod]
        public void should_not_be_valid()
        {
            Assert.IsFalse(isValid); 
        }

        [TestMethod]
        public void should_not_call_the_database()
        {
            this.repository.Verify(x => x.FindOne<Location>(It.IsAny<object>()), Times.Never());
        }

        [TestMethod]
        public void should_have_the_correct_message()
        {
            Assert.AreEqual("Market requires a valid Location", this.Sut.Message);
        }
    }

    [TestClass]
    public class when_a_request_is_made_to_validate_a_null_nexus_id_that_is_not_required : SpecBase<NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Location, Location, LocationMapping>>
    {
        private RWEST.Nexus.MDM.Contracts.Location location;
        private bool isValid;
        private Mock<IRepository> repository;

        protected override NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Location, MDM.Location, LocationMapping> Establish_context()
        {
            this.repository = new Mock<IRepository>();
            this.repository.Setup(repository => repository.FindOne<Location>(1)).Returns(new Location() { Id = 1 });

            this.location = new RWEST.Nexus.MDM.Contracts.Location()
                {
                    Details =
                        new LocationDetails()
                            {
                                Parent = null
                            }
                };

            return new NexusEntityExistsRule<RWEST.Nexus.MDM.Contracts.Location, Location, LocationMapping>(this.repository.Object, market => this.location.Details.Parent, false);
        }

        protected override void Because_of()
        {
            this.isValid = this.Sut.IsValid(this.location);
        }

        [TestMethod]
        public void should_be_valid()
        {
            Assert.IsTrue(isValid); 
        }

        [TestMethod]
        public void should_not_call_the_database()
        {
            this.repository.Verify(x => x.FindOne<Location>(It.IsAny<object>()), Times.Never());
        }

        [TestMethod]
        public void should_have_no_message()
        {
            Assert.AreEqual(null, this.Sut.Message);
        }
    }
}
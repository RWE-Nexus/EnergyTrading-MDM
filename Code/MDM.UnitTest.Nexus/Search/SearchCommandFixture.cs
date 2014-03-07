namespace RWEST.Nexus.MDM.Test.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using RWEST.Nexus.Data;
    using RWEST.Nexus.Data.EntityFramework;
    using RWEST.Nexus.MDM.Data.EF.Configuration;
    using RWEST.Nexus.MDM.Data.Search;
    using RWEST.Nexus.MDM.Test.Data.EF;
    using RWEST.Nexus.Test;

    [TestClass]
    public class when_a_search_command_is_executed :
        SpecBase<SearchCommand<Party, PartyDetails, PartyMapping>>
    {
        private Mock<IRepository> repository;

        //[TestMethod]
        //public void should_return_the_expected_entity()
        //{
        //    Assert.AreEqual(repository.FindOne<Party>(this.entityIds[0]).Details[0].Name, this.party1.Details[0].Name);
        //}

        protected override SearchCommand<Party, PartyDetails, PartyMapping> Establish_context()
        {
            this.repository = new Mock<IRepository>();
            return new SearchCommand<Party, PartyDetails, PartyMapping>(this.repository.Object);
        }

        protected override void Because_of()
        {
            this.Sut.Execute("abc", null, null, null);
        }
    }
}

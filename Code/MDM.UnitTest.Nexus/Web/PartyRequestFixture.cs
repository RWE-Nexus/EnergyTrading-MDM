namespace EnergyTrading.MDM.Test.Web
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.ServiceModel.Web;

    using global::MDM.ServiceHost.Wcf.Feeds;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Web;
    using EnergyTrading.MDM.Messages;
    using EnergyTrading.MDM.Services;

    using PartyService = EnergyTrading.MDM.ServiceHost.Wcf.Nexus.PartyService;

    [TestClass]
    public class PartyRequestFixture : ContractRequestFixture<Party, MDM.Party>
    {
        [TestMethod]
        public void UnsuccessfulMatchReturnsNotFound()
        {
            // Arrange
            var wrapper = new Mock<IWebOperationContextWrapper>();
            var service = new Mock<IMdmService<Party, MDM.Party>>();
            var feedFactory = new Mock<IFeedFactory>();
            Container.RegisterInstance(wrapper.Object);
            Container.RegisterInstance(service.Object);
            Container.RegisterInstance(feedFactory.Object);

            wrapper.Setup(contextWrapper => contextWrapper.QueryParameters)
                   .Returns(() => new NameValueCollection());
            wrapper.Setup(x => x.Headers)
                   .Returns(() => new WebHeaderCollection());

            var contract = new ContractResponse<Party>
            {
                Error = new ContractError
                {
                    Type = ErrorType.NotFound
                },
                IsValid = false
            };
            service.Setup(x => x.Request(It.IsAny<GetRequest>())).Returns(contract);

            var wcfService = new PartyService();

            // Act
            try
            {
                wcfService.Get("1");
            }
            catch (WebFaultException<Fault> ex)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, ex.StatusCode, "Status code differs");
            }
        }
    }
}
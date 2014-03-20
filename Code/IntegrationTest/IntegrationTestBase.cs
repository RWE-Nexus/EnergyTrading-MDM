namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.ServiceModel.Web;

    using EnergyTrading.MDM.ServiceHost.Wcf.Nexus;

    using NUnit.Framework;

    [SetUpFixture]
    public abstract class IntegrationTestBase
    {
        protected static string DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.fffffffZ";
        protected static ObjectScript Script;
        protected static Dictionary<string, string> ServiceUrl;
        private static List<WebServiceHost> webServiceHosts = new List<WebServiceHost>();

        [SetUp]
        public static void CreateServiceHost(TestContext context)
        {
            // TODO_IntegrationTest - add unique url for new entity
            ServiceUrl = new Dictionary<string, string>
                {
                    { "SourceSystem", "http://127.0.0.1:8013/" },
                    { "ReferenceData", "http://127.0.0.1:8014/" }
                };

            // TODO_IntegrationTest - add WebServiceHost for new entity
            webServiceHosts.Add(new WebServiceHost(typeof(SourceSystemService), new Uri(ServiceUrl["SourceSystem"])));
            webServiceHosts.Add(new WebServiceHost(typeof(ReferenceDataService), new Uri(ServiceUrl["ReferenceData"])));

            Script = new ObjectScript();
            Script.RunScript();

            var global = new GlobalMock();
            global.Application_Start();

            foreach(var host in webServiceHosts)
            {
                IncludeExceptionDetailInFaults(host);

                host.Open();
            }
        }

        private static void IncludeExceptionDetailInFaults(ServiceHostBase host)
        {
            var behavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();

            if (behavior == null)
            {
                behavior = new ServiceDebugBehavior();
                host.Description.Behaviors.Add(behavior);
            }

            behavior.IncludeExceptionDetailInFaults = true;
        }

        [TearDown]
        public static void CloseServiceHost()
        {
            foreach(var host in webServiceHosts)
            {
                host.Close();
            }
        }
    }
}
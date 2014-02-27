namespace SelfHost
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.ServiceModel.Web;

    using RWEST.Nexus.MDM.MappingService;

    internal class Program
    {
        #region Methods

        private static void Main(string[] args)
        {
            new GlobalMock();
            var host = new WebServiceHost(typeof(PersonService), new Uri("http://localhost:8000"));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(PersonService), new WebHttpBinding(), "Person");
//             ServiceDebugBehavior stp = host.Description.Behaviors.Find();
//             stp.HttpHelpPageEnabled = false;
            host.Open();
            Console.WriteLine("Service is up and running");
            Console.WriteLine("Press enter to quit ");
            Console.ReadLine();
            host.Close();
        }

        public class GlobalMock : Global
        {
            public GlobalMock()
            {
                this.Application_Start(this, null);
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MDM.AdcGateway.Configuration;

namespace MDM.AdcGateway
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var serviceConfiguration = new ServiceConfiguration(ContainerConfiguration.Create());
            serviceConfiguration.Configure();

            Application.Run(new FormMain());
        }
    }
}

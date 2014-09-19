namespace MDM.ServiceHost.WebApi.Configuration
{
    using EnergyTrading.Container.Unity;

    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public static class ContainerConfiguration
    {
        public static UnityContainer Create()
        {
            var container = new UnityContainer();
            container.InstallCoreExtensions();

            // Configurator will read Enterprise Library configuration 
            // and set up the container
            var configurator = new UnityContainerConfigurator(container);

            // Configuration source holds the new configuration we want to use 
            // load this in your own code
            IConfigurationSource configSource = new SystemConfigurationSource(true);

            // Configure the container
            EnterpriseLibraryContainer.ConfigureContainer(configurator, configSource);

            // Self-register and set up service location 
            container.RegisterInstance<IUnityContainer>(container);
            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            return container;
        }
    }
}
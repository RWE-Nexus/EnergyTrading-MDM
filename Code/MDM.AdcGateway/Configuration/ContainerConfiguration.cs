namespace MDM.AdcGateway.Configuration
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Container.Unity;

    public static class ContainerConfiguration
    {
        public static UnityContainer Create()
        {
            var container = new UnityContainer();

            container.InstallCoreExtensions();

            // Self-register and set up service location 
            container.RegisterInstance<IUnityContainer>(container);
            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            return container;
        }
    }
}
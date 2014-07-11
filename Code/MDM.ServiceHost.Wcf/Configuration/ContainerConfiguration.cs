namespace EnergyTrading.Mdm.ServiceHost.Wcf.Configuration
{
    using EnergyTrading.Container.Unity;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    public static class ContainerConfiguration
    {
        public static UnityContainer Create()
        {
            var container = new UnityContainer();
            container.StandardConfiguration();

            var locator = container.Resolve<IServiceLocator>();

            // NB Need this for WCF service host stuff.
            ServiceLocator.SetLocatorProvider(() => locator);

            return container;
        }
    }
}
namespace EnergyTrading.MDM.Test.Web
{
    using EnergyTrading.MDM.ServiceHost.Wcf.Nexus;

    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContractRequestFixture<TContract, TEntity>
        where TContract : class
    {
        protected UnityContainer Container { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Container = new UnityContainer();

            // Self-register and set up service location 
            Container.RegisterInstance<IUnityContainer>(Container);
            var locator = new UnityServiceLocator(Container);
            Global.ServiceLocator = locator;
        }
    }
}

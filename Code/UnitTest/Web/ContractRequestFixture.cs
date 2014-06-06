namespace EnergyTrading.Mdm.Test.Web
{
    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    [TestFixture]
    public class ContractRequestFixture<TContract, TEntity>
        where TContract : class
    {
        protected UnityContainer Container { get; set; }

        [SetUp]
        public void Setup()
        {
            Container = new UnityContainer();

            // Self-register and set up service location 
            Container.RegisterInstance<IUnityContainer>(Container);
            var locator = new UnityServiceLocator(Container);
        }
    }
}

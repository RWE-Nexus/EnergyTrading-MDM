namespace EnergyTrading.MDM.Test.Configuration
{
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.MDM.Configuration;
    using EnergyTrading.Configuration;

    [TestClass]
    public class PersonConfigurationFixture : EntityConfigurationFixture
    {
        protected override string EntityName
        {
            get { return "person"; }
        }

        protected override IConfigurationTask CreateConfiguration(IUnityContainer container)
        {
            return new PersonConfiguration(container);
        }
    }
}
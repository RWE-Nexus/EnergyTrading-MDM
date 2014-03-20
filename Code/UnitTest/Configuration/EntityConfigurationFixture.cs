namespace EnergyTrading.MDM.Test.Configuration
{
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;

    using Microsoft.Practices.Unity;
    using NUnit.Framework;

    using Moq;

    using EnergyTrading.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Validation;
    using EnergyTrading.MDM.Messages;

    [TestFixture]
    public abstract class EntityConfigurationFixture : Fixture
    {
        protected abstract string EntityName { get; }

        [Test]
        public void ResolveValidatorEngine()
        {
            this.Resolve<IValidatorEngine>();
        }

        [Test]
        public void ResolveMappingRequestValidator()
        {
            this.Resolve<IValidator<CreateMappingRequest>>();
        }

        [Test]
        public void ResolveNexusMappingValidator()
        {
            this.Resolve<IValidator<EnergyTrading.Mdm.Contracts.MdmId>>();
        }

        protected void Resolve<T>()
        {
            var result = Container.Resolve<T>(EntityName);
            Assert.IsNotNull(result,  typeof(T).Name + " did not resolve for " + EntityName);
        }

        protected abstract IConfigurationTask CreateConfiguration(IUnityContainer container);

        protected override void OnSetup()
        {
            // Pre-requisites for the entity configuration
            var mapping = new SimpleMappingEngineConfiguration(this.Container);
            mapping.Configure();

            var repository = new Mock<IRepository>();
            this.Container.RegisterInstance(repository.Object);
            
            // Register the entities stuff
            var config = this.CreateConfiguration(this.Container);
            config.Configure();
        }
    }
}
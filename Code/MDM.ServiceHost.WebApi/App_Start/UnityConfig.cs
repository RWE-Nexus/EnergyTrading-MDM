using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EnergyTrading.Configuration;
using EnergyTrading.Container.Unity.AutoRegistration;
using EnergyTrading.Mdm.ServiceHost.Unity.Configuration;
using EnergyTrading.Web;
using MDM.ServiceHost.WebApi.Configuration;
using MDM.ServiceHost.WebApi.Infrastructure.Configuration;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace MDM.ServiceHost.WebApi
{
    public class UnityConfig
    {
        public virtual IUnityContainer RegisterComponents(HttpConfiguration httpConfig, IUnityContainer aContainer = null)
        {
            var container = aContainer ?? ContainerConfiguration.Create();
            container.RegisterTypes(
                AllClasses.FromAssemblies(Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(ContainerConfiguration)),
                    Assembly.GetAssembly(typeof(SourceSystemConfiguration)),
                    Assembly.GetAssembly(typeof(SimpleMappingEngineConfiguration)))
                    .Where(t => t.Implements<IGlobalConfigurationTask>()),
                WithMappings.FromAllInterfaces,
                ConfiguratorName,
                WithLifetime.ContainerControlled);

            container.RegisterType<IWebOperationContextWrapper, WebOperationContextWrapper>();
            container.RegisterType<IConfigurationManager, AppConfigConfigurationManager>(new ContainerControlledLifetimeManager());
            RegisterMdmServiceAssemblyNameLocators(container);
            AdditionalRegistrations(container);

            // Now get them all, and initialize them, bootstrapper takes care of ordering
            var globalTasks = container.ResolveAll<IGlobalConfigurationTask>().ToList();
            var tasks = globalTasks.Select(task => task as IConfigurationTask).ToList();
            LogConfigurationTasks(tasks);
            ConfigurationBootStrapper.Initialize(tasks);

            httpConfig.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            return container;
        }

        protected virtual void RegisterMdmServiceAssemblyNameLocators(IUnityContainer container)
        {
            container.RegisterType<IContractAssemblyNamesLocator, ConfigSettingsContractAssemblyNamesLocator>();
            container.RegisterType<IEntityAssemblyNamesLocator, ConfigSettingsEntityAssemblyNamesLocator>();
        }

        protected virtual void AdditionalRegistrations(IUnityContainer container)
        {

        }

        protected virtual void LogConfigurationTasks(IEnumerable<IConfigurationTask> tasks)
        {
            
        }

        protected virtual string ConfiguratorName(Type configurator)
        {
            var split = Regex.Split(configurator.Namespace, ".V[\\d]+");
            return split.Length > 1 ? string.Format("{0}{1}", configurator.Name, configurator.Namespace.Substring(configurator.Namespace.LastIndexOf(".V"))) : configurator.Name;
        }
    }
}
namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    using EnergyTrading.Configuration;
    using EnergyTrading.MDM.Configuration;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration;

    using global::MDM.ServiceHost.Wcf.Configuration;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using SourceSystemConfiguration = EnergyTrading.MDM.ServiceHost.Unity.Configuration.SourceSystemConfiguration;

    public class Global : HttpApplication
    {
        public static IServiceLocator ServiceLocator { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            // Self-register and set up service location 
            var container = ContainerConfiguration.Create();

            // Auto register all the global configuration tasks
            //container.ConfigureAutoRegistration()
            //     .Include(x => x.Implements<IGlobalConfigurationTask>(),
            //              Then.Register().As<IGlobalConfigurationTask>().WithTypeName())
            //     .ApplyAutoRegistration(new[] { GetType().Assembly, typeof(BrokerCommodityConfiguration).Assembly });

            // Register all IGlobalConfigurationTasks from this and any other appropriate assemblies
            // NB This needs fixing so we don't have to name them - needs specific IDependencyResolver
            // see http://www.chrisvandesteeg.nl/2009/04/16/making-unity-work-more-like-the-others/
            container.RegisterType<IGlobalConfigurationTask, NexusMappingContextConfiguration>("a");
            container.RegisterType<IGlobalConfigurationTask, RouteConfiguration>("b");
            container.RegisterType<IGlobalConfigurationTask, ServiceConfiguration>("c");
            container.RegisterType<IGlobalConfigurationTask, SimpleMappingEngineConfiguration>("d");
            container.RegisterType<IGlobalConfigurationTask, RepositoryConfiguration>("e");
            // container.RegisterType<IGlobalConfigurationTask, ProfilingConfiguration>("e");
            container.RegisterType<IGlobalConfigurationTask, LoggerConfiguration>("f");

            // Per-entity configurations
            // TODO_CodeGeneration - Add entity configuration
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.AgreementConfiguration>("agreement");
            container.RegisterType<IGlobalConfigurationTask, BookConfiguration>("book");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.BrokerCommodityConfiguration>("brokercommodity");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.BrokerRateConfiguration>("brokerrate");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.BusinessUnitConfiguration>("businessunit");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.BrokerConfiguration>("broker");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.CalendarConfiguration>("calendar");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.CurveConfiguration>("curve");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.CommodityConfiguration>("commodity");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.CommodityInstrumentTypeConfiguration>("commodityinstrumenttype");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.CounterpartyConfiguration>("counterparty");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.BookDefaultConfiguration>("bookdefault");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.DimensionConfiguration>("dimension");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ExchangeConfiguration>("exchange");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.HierarchyConfiguration>("hierarchy");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.InstrumentTypeConfiguration>("instrumenttype");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.InstrumentTypeOverrideConfiguration>("instrumenttypeoverride");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.LocationConfiguration>("location");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.LocationRoleConfiguration>("locationrole");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.MarketConfiguration>("market");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PartyConfiguration>("party");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PartyAccountabilityConfiguration>("partyaccountability");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PartyRoleConfiguration>("partyrole");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PartyRoleAccountabilityConfiguration>("partyroleaccountability");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PartyOverrideConfiguration>("partyoverride");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PersonConfiguration>("person");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ProductConfiguration>("product");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ProductCurveConfiguration>("productcurve");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ProductTypeConfiguration>("producttype");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ProductTypeInstanceConfiguration>("producttypeinstance");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ProductScotaConfiguration>("productscota");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PortfolioConfiguration>("portfolio");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.PortfolioHierarchyConfiguration>("portfoliohierarchy");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.SettlementContactConfiguration>("settlementcontact");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ShapeConfiguration>("shape");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ShapeDayConfiguration>("shapeday");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ShapeElementConfiguration>("shapeelement");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.ShipperCodeConfiguration>("shippercode");
            container.RegisterType<IGlobalConfigurationTask, SourceSystemConfiguration>("sourcesystem");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.UnitConfiguration>("unit");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.VesselConfiguration>("vessel");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.FeeTypeConfiguration>("feetype");
            container.RegisterType<IGlobalConfigurationTask, MDM.Configuration.CommodityFeeTypeConfiguration>("commodityfeetype");
            container.RegisterType<IGlobalConfigurationTask, TenorConfiguration>("tenor");
            container.RegisterType<IGlobalConfigurationTask, TenorTypeConfiguration>("tenortype");
            container.RegisterType<IGlobalConfigurationTask, ProductTenorTypeConfiguration>("producttenortype");
            container.RegisterType<IGlobalConfigurationTask, LegalEntityConfiguration>("legalentity");

            // Some dependencies for the tasks
            container.RegisterInstance(RouteTable.Routes);

            // Now get them all, and initialize them, bootstrapper takes care of ordering
            var globalTasks = container.ResolveAll<IGlobalConfigurationTask>();
            var tasks = globalTasks.Select(task => task as IConfigurationTask).ToList();

            ConfigurationBootStrapper.Initialize(tasks);

            ServiceLocator = container.Resolve<IServiceLocator>();
        }
    }
}
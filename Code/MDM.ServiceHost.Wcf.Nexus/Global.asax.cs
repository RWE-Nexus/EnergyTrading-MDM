namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    using EnergyTrading.Configuration;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.MDM.ServiceHost.Unity.Configuration;
    using EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration;

    using global::MDM.ServiceHost.Unity.Nexus.Configuration;
    using global::MDM.ServiceHost.Wcf.Configuration;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using CalendarConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.CalendarConfiguration;
    using CommodityConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.CommodityConfiguration;
    using CommodityInstrumentTypeConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.CommodityInstrumentTypeConfiguration;
    using CurveConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.CurveConfiguration;
    using DimensionConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.DimensionConfiguration;
    using HierarchyConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.HierarchyConfiguration;
    using InstrumentTypeConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.InstrumentTypeConfiguration;
    using InstrumentTypeOverrideConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.InstrumentTypeOverrideConfiguration;
    using LocationConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.LocationConfiguration;
    using LocationRoleConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.LocationRoleConfiguration;
    using MarketConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.MarketConfiguration;
    using PartyAccountabilityConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PartyAccountabilityConfiguration;
    using PartyConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PartyConfiguration;
    using PartyRoleAccountabilityConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PartyRoleAccountabilityConfiguration;
    using PartyRoleConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PartyRoleConfiguration;
    using PersonConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PersonConfiguration;
    using PortfolioConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PortfolioConfiguration;
    using PortfolioHierarchyConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.PortfolioHierarchyConfiguration;
    using ProductConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.ProductConfiguration;
    using ProductCurveConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.ProductCurveConfiguration;
    using ProductTypeConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.ProductTypeConfiguration;
    using ProductTypeInstanceConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.ProductTypeInstanceConfiguration;
    using SettlementContactConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.SettlementContactConfiguration;
    using ShipperCodeConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.ShipperCodeConfiguration;
    using SourceSystemConfiguration = EnergyTrading.MDM.ServiceHost.Unity.Configuration.SourceSystemConfiguration;
    using UnitConfiguration = global::MDM.ServiceHost.Unity.Nexus.Configuration.UnitConfiguration;

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
            container.RegisterType<IGlobalConfigurationTask, AutoMapperConfiguration>("automapper");
            container.RegisterType<IGlobalConfigurationTask, RouteConfiguration>("b");
            container.RegisterType<IGlobalConfigurationTask, ServiceConfiguration>("c");
            container.RegisterType<IGlobalConfigurationTask, SimpleMappingEngineConfiguration>("d");
            container.RegisterType<IGlobalConfigurationTask, RepositoryConfiguration>("e");
            // container.RegisterType<IGlobalConfigurationTask, ProfilingConfiguration>("e");
            container.RegisterType<IGlobalConfigurationTask, LoggerConfiguration>("f");

            // Per-entity configurations
            // TODO_CodeGeneration - Add entity configuration
            container.RegisterType<IGlobalConfigurationTask, AgreementConfiguration>("agreement");
            container.RegisterType<IGlobalConfigurationTask, BookConfiguration>("book");
            container.RegisterType<IGlobalConfigurationTask, BrokerCommodityConfiguration>("brokercommodity");
            container.RegisterType<IGlobalConfigurationTask, BrokerRateConfiguration>("brokerrate");
            container.RegisterType<IGlobalConfigurationTask, BusinessUnitConfiguration>("businessunit");
            container.RegisterType<IGlobalConfigurationTask, BrokerConfiguration>("broker");
            container.RegisterType<IGlobalConfigurationTask, CalendarConfiguration>("calendar");
            container.RegisterType<IGlobalConfigurationTask, CurveConfiguration>("curve");
            container.RegisterType<IGlobalConfigurationTask, CommodityConfiguration>("commodity");
            container.RegisterType<IGlobalConfigurationTask, CommodityInstrumentTypeConfiguration>("commodityinstrumenttype");
            container.RegisterType<IGlobalConfigurationTask, CounterpartyConfiguration>("counterparty");
            container.RegisterType<IGlobalConfigurationTask, BookDefaultConfiguration>("bookdefault");
            container.RegisterType<IGlobalConfigurationTask, DimensionConfiguration>("dimension");
            container.RegisterType<IGlobalConfigurationTask, ExchangeConfiguration>("exchange");
            container.RegisterType<IGlobalConfigurationTask, HierarchyConfiguration>("hierarchy");
            container.RegisterType<IGlobalConfigurationTask, InstrumentTypeConfiguration>("instrumenttype");
            container.RegisterType<IGlobalConfigurationTask, InstrumentTypeOverrideConfiguration>("instrumenttypeoverride");
            container.RegisterType<IGlobalConfigurationTask, LocationConfiguration>("location");
            container.RegisterType<IGlobalConfigurationTask, LocationRoleConfiguration>("locationrole");
            container.RegisterType<IGlobalConfigurationTask, MarketConfiguration>("market");
            container.RegisterType<IGlobalConfigurationTask, PartyConfiguration>("party");
            container.RegisterType<IGlobalConfigurationTask, PartyAccountabilityConfiguration>("partyaccountability");
            container.RegisterType<IGlobalConfigurationTask, PartyRoleConfiguration>("partyrole");
            container.RegisterType<IGlobalConfigurationTask, PartyRoleAccountabilityConfiguration>("partyroleaccountability");
            container.RegisterType<IGlobalConfigurationTask, PartyOverrideConfiguration>("partyoverride");
            container.RegisterType<IGlobalConfigurationTask, PersonConfiguration>("person");
            container.RegisterType<IGlobalConfigurationTask, ProductConfiguration>("product");
            container.RegisterType<IGlobalConfigurationTask, ProductCurveConfiguration>("productcurve");
            container.RegisterType<IGlobalConfigurationTask, ProductTypeConfiguration>("producttype");
            container.RegisterType<IGlobalConfigurationTask, ProductTypeInstanceConfiguration>("producttypeinstance");
            container.RegisterType<IGlobalConfigurationTask, ProductScotaConfiguration>("productscota");
            container.RegisterType<IGlobalConfigurationTask, PortfolioConfiguration>("portfolio");
            container.RegisterType<IGlobalConfigurationTask, PortfolioHierarchyConfiguration>("portfoliohierarchy");
            container.RegisterType<IGlobalConfigurationTask, SettlementContactConfiguration>("settlementcontact");
            container.RegisterType<IGlobalConfigurationTask, ShapeConfiguration>("shape");
            container.RegisterType<IGlobalConfigurationTask, ShapeDayConfiguration>("shapeday");
            container.RegisterType<IGlobalConfigurationTask, ShapeElementConfiguration>("shapeelement");
            container.RegisterType<IGlobalConfigurationTask, ShipperCodeConfiguration>("shippercode");
            container.RegisterType<IGlobalConfigurationTask, SourceSystemConfiguration>("sourcesystem");
            container.RegisterType<IGlobalConfigurationTask, UnitConfiguration>("unit");
            container.RegisterType<IGlobalConfigurationTask, VesselConfiguration>("vessel");
            container.RegisterType<IGlobalConfigurationTask, FeeTypeConfiguration>("feetype");
            container.RegisterType<IGlobalConfigurationTask, CommodityFeeTypeConfiguration>("commodityfeetype");
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
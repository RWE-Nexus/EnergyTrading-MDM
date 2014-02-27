using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using EnergyTrading.Configuration;
using EnergyTrading.MDM.Configuration;
using EnergyTrading.MDM.Data.EF.Configuration;
using EnergyTrading.MDM.MappingService2.Configuration;
using Microsoft.Practices.Unity;

namespace EnergyTrading.MDM.MappingService2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
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
            //container.RegisterType<IGlobalConfigurationTask, RouteConfiguration>("b");
            container.RegisterType<IGlobalConfigurationTask, ServiceConfiguration>("c");
            container.RegisterType<IGlobalConfigurationTask, SimpleMappingEngineConfiguration>("d");
            container.RegisterType<IGlobalConfigurationTask, RepositoryConfiguration>("e");
            // container.RegisterType<IGlobalConfigurationTask, ProfilingConfiguration>("e");
            // container.RegisterType<IGlobalConfigurationTask, LoggerConfiguration>("f");

            // Per-entity configurations
            // TODO_CodeGeneration - Add entity configuration
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.AgreementConfiguration>("agreement");
            container.RegisterType<IGlobalConfigurationTask, BookConfiguration>("book");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.BrokerCommodityConfiguration>("brokercommodity");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.BrokerRateConfiguration>("brokerrate");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.BusinessUnitConfiguration>("businessunit");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.BrokerConfiguration>("broker");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.CalendarConfiguration>("calendar");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.CurveConfiguration>("curve");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.CommodityConfiguration>("commodity");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.CommodityInstrumentTypeConfiguration>("commodityinstrumenttype");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.CounterpartyConfiguration>("counterparty");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.BookDefaultConfiguration>("bookdefault");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.DimensionConfiguration>("dimension");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ExchangeConfiguration>("exchange");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.HierarchyConfiguration>("hierarchy");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.InstrumentTypeConfiguration>("instrumenttype");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.InstrumentTypeOverrideConfiguration>("instrumenttypeoverride");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.LocationConfiguration>("location");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.LocationRoleConfiguration>("locationrole");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.MarketConfiguration>("market");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PartyConfiguration>("party");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PartyAccountabilityConfiguration>("partyaccountability");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PartyRoleConfiguration>("partyrole");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PartyRoleAccountabilityConfiguration>("partyroleaccountability");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PartyOverrideConfiguration>("partyoverride");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PersonConfiguration>("person");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ProductConfiguration>("product");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ProductCurveConfiguration>("productcurve");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ProductTypeConfiguration>("producttype");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ProductTypeInstanceConfiguration>("producttypeinstance");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ProductScotaConfiguration>("productscota");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PortfolioConfiguration>("portfolio");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.PortfolioHierarchyConfiguration>("portfoliohierarchy");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.SettlementContactConfiguration>("settlementcontact");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ShapeConfiguration>("shape");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ShapeDayConfiguration>("shapeday");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ShapeElementConfiguration>("shapeelement");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.ShipperCodeConfiguration>("shippercode");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.SourceSystemConfiguration>("sourcesystem");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.UnitConfiguration>("unit");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.VesselConfiguration>("vessel");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.FeeTypeConfiguration>("feetype");
            container.RegisterType<IGlobalConfigurationTask, EnergyTrading.MDM.Configuration.CommodityFeeTypeConfiguration>("commodityfeetype");
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

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
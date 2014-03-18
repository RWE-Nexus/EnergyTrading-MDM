namespace EnergyTrading.MDM.Data.EF.Configuration
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class NexusMappingContext : DbContext
    {
        public NexusMappingContext() //: base("name=MappingContext")
        {
            if (ProfileDb)
            {
                HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            }
        }

        protected static bool ProfileDb
        {
            get
            {
                return ConfigurationManager.AppSettings["profile.db"] == "true";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<DateRange>();

            // Stop it checking the schema
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            // TODO: Use AutoRegistration to find these

            // TODO_CodeGeneration - Add configuration classes
            modelBuilder.Configurations.Add(new AgreementConfiguration());
            modelBuilder.Configurations.Add(new AgreementMappingConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new BookMappingConfiguration());
            modelBuilder.Configurations.Add(new BrokerCommodityConfiguration());
            modelBuilder.Configurations.Add(new BrokerCommodityMappingConfiguration());
            modelBuilder.Configurations.Add(new BrokerRateConfiguration());
            modelBuilder.Configurations.Add(new BrokerRateDetailsConfiguration());
            modelBuilder.Configurations.Add(new BrokerRateMappingConfiguration());
            modelBuilder.Configurations.Add(new CalendarConfiguration());
            modelBuilder.Configurations.Add(new CalendarDayConfiguration());
            modelBuilder.Configurations.Add(new CalendarMappingConfiguration());
            modelBuilder.Configurations.Add(new CurveConfiguration());
            modelBuilder.Configurations.Add(new CurveMappingConfiguration());
            modelBuilder.Configurations.Add(new CommodityConfiguration());
            modelBuilder.Configurations.Add(new CommodityMappingConfiguration());
            modelBuilder.Configurations.Add(new CommodityInstrumentTypeConfiguration());
            modelBuilder.Configurations.Add(new CommodityInstrumentTypeMappingConfiguration());
            modelBuilder.Configurations.Add(new BookDefaultConfiguration());
            modelBuilder.Configurations.Add(new BookDefaultMappingConfiguration());
            modelBuilder.Configurations.Add(new DimensionConfiguration());
            modelBuilder.Configurations.Add(new DimensionMappingConfiguration());
            modelBuilder.Configurations.Add(new HierarchyConfiguration());
            modelBuilder.Configurations.Add(new HierarchyMappingConfiguration());
            modelBuilder.Configurations.Add(new InstrumentTypeConfiguration());
            modelBuilder.Configurations.Add(new InstrumentTypeMappingConfiguration()); 
            modelBuilder.Configurations.Add(new InstrumentTypeOverrideConfiguration());
            modelBuilder.Configurations.Add(new InstrumentTypeOverrideMappingConfiguration()); 
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new LocationMappingConfiguration());
            modelBuilder.Configurations.Add(new LocationRoleConfiguration());
            modelBuilder.Configurations.Add(new LocationRoleMappingConfiguration());
            modelBuilder.Configurations.Add(new LocationRoleTypeConfiguration());
            modelBuilder.Configurations.Add(new MarketConfiguration());
            modelBuilder.Configurations.Add(new MarketMappingConfiguration());
            modelBuilder.Configurations.Add(new PartyConfiguration());
            modelBuilder.Configurations.Add(new PartyDetailsConfiguration());
            modelBuilder.Configurations.Add(new PartyMappingConfiguration());
            modelBuilder.Configurations.Add(new PartyAccountabilityConfiguration());
            modelBuilder.Configurations.Add(new PartyAccountabilityMappingConfiguration());
            modelBuilder.Configurations.Add(new SettlementContactConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleDetailsConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleAccountabilityConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleAccountabilityMappingConfiguration());
            modelBuilder.Configurations.Add(new BrokerDetailsConfiguration());
            modelBuilder.Configurations.Add(new ExchangeDetailsConfiguration());
            modelBuilder.Configurations.Add(new BusinessUnitDetailsConfiguration());
            modelBuilder.Configurations.Add(new CounterpartyDetailsConfiguration());
            modelBuilder.Configurations.Add(new PartyOverrideConfiguration());
            modelBuilder.Configurations.Add(new PartyOverrideMappingConfiguration());
            modelBuilder.Configurations.Add(new PartyRoleMappingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonDetailsConfiguration());
            modelBuilder.Configurations.Add(new PersonMappingConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductCurveConfiguration());
            modelBuilder.Configurations.Add(new ProductCurveMappingConfiguration());
            modelBuilder.Configurations.Add(new ProductMappingConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeInstanceConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeInstanceMappingConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeMappingConfiguration());
            modelBuilder.Configurations.Add(new ProductScotaConfiguration());
            modelBuilder.Configurations.Add(new ProductScotaMappingConfiguration());
            modelBuilder.Configurations.Add(new PortfolioConfiguration());
            modelBuilder.Configurations.Add(new PortfolioMappingConfiguration());
            modelBuilder.Configurations.Add(new PortfolioHierarchyConfiguration());
            modelBuilder.Configurations.Add(new PortfolioHierarchyMappingConfiguration());
            modelBuilder.Configurations.Add(new ShapeConfiguration());
            modelBuilder.Configurations.Add(new ShapeMappingConfiguration());
            modelBuilder.Configurations.Add(new ShapeDayConfiguration());
            modelBuilder.Configurations.Add(new ShapeDayMappingConfiguration());
            modelBuilder.Configurations.Add(new ShapeElementConfiguration());
            modelBuilder.Configurations.Add(new ShapeElementMappingConfiguration());
            modelBuilder.Configurations.Add(new ShipperCodeConfiguration());
            modelBuilder.Configurations.Add(new ShipperCodeMappingConfiguration());
            modelBuilder.Configurations.Add(new SourceSystemConfiguration());
            modelBuilder.Configurations.Add(new SourceSystemMappingConfiguration());
            modelBuilder.Configurations.Add(new ReferenceDataConfiguration());
            modelBuilder.Configurations.Add(new UnitConfiguration());
            modelBuilder.Configurations.Add(new UnitMappingConfiguration());
            modelBuilder.Configurations.Add(new VesselConfiguration());
            modelBuilder.Configurations.Add(new VesselMappingConfiguration());
            modelBuilder.Configurations.Add(new FeeTypeConfiguration());
            modelBuilder.Configurations.Add(new FeeTypeMappingConfiguration());
            modelBuilder.Configurations.Add(new CommodityFeeTypeConfiguration());
            modelBuilder.Configurations.Add(new CommodityFeeTypeMappingConfiguration());
            modelBuilder.Configurations.Add(new TenorConfiguration());
            modelBuilder.Configurations.Add(new TenorMappingConfiguration());
            modelBuilder.Configurations.Add(new TenorTypeConfiguration());
            modelBuilder.Configurations.Add(new TenorTypeMappingConfiguration());
            modelBuilder.Configurations.Add(new ProductTenorTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductTenorTypeMappingConfiguration());
            modelBuilder.Configurations.Add(new LegalEntityDetailsConfiguration());
            //modelBuilder.Entity<LocationRole>()
            //    .Map<LocationRole>(m => m.Requires("LocationRoleClass").HasValue("LocationRole"))
            //    .ToTable("LocationRole");

            //modelBuilder.Entity<PartyRole>()
            //    .Map<PartyRole>(m => m.Requires("PartyRoleClass").HasValue("PartyRole"))
            //    .ToTable("PartyRole");
        }
    }
}
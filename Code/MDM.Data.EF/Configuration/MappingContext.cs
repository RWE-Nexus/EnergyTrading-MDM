namespace EnergyTrading.Mdm.Data.EF.Configuration
{
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using EnergyTrading;

    public class MappingContext : DbContext
    {
        public MappingContext() //: base("name=MappingContext")
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
            modelBuilder.Configurations.Add(new SourceSystemConfiguration());
            modelBuilder.Configurations.Add(new SourceSystemMappingConfiguration());
        }
    }
}
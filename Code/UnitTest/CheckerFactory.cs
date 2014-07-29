namespace EnergyTrading.Mdm.Test
{
    using System.Reflection;

    using EnergyTrading.Data;

    using NCheck.Checking;

    public class CheckerFactory : EnergyTrading.Test.CheckerFactory
    {
        public CheckerFactory()
        {
            // NB Conventions must be before assembly registration for registrations to obey them.
            Convention((PropertyInfo x) => x.Name == "Timestamp", CompareTarget.Ignore);
            Convention((PropertyInfo x) => x.Name == "Version", CompareTarget.Ignore);
            Convention((PropertyInfo x) => x.Name == "Mappings", CompareTarget.Count);
            Convention(x => typeof(IIdentifiable).IsAssignableFrom(x), CompareTarget.Id);
        }
    }
}
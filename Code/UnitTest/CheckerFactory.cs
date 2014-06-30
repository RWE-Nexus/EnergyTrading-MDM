namespace EnergyTrading.Mdm.Test
{
    using System.Reflection;

    using EnergyTrading.Data;
    using EnergyTrading.Test.Data;

    using NCheck.Checking;

    public class CheckerFactory : EnergyTrading.Test.CheckerFactory
    {
        public CheckerFactory()
        {
            PropertyCheck.IdentityChecker = new IdentifiableChecker();

            // NB Conventions must be before assembly registration for registrations to obey them.
            Convention((PropertyInfo x) => x.Name == "Timestamp" ? CompareTarget.Ignore : CompareTarget.Unknown);
            Convention((PropertyInfo x) => x.Name == "Version" ? CompareTarget.Ignore : CompareTarget.Unknown);
            Convention((PropertyInfo x) => x.Name == "Mappings" ? CompareTarget.Count : CompareTarget.Unknown);
            Convention(x => typeof(IIdentifiable).IsAssignableFrom(x) ? CompareTarget.Id : CompareTarget.Unknown);
        }
    }
}
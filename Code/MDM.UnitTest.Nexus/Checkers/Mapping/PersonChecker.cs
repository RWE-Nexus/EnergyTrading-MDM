namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PersonChecker : Checker<Person>
    {
        public PersonChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Details).Count();
            Compare(x => x.Mappings).Count();
        }
    }
}
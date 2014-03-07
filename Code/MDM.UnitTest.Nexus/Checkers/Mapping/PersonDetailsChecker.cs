namespace EnergyTrading.MDM.Test.Checkers.Mapping
{
    using RWEST.Nexus.MDM;
    using EnergyTrading.Test;

    public class PersonDetailsChecker : Checker<PersonDetails>
    {
        public PersonDetailsChecker()
        {
            Compare(x => x.Id);
            Compare(x => x.Person).Id();
            Compare(x => x.FirstName);
            Compare(x => x.LastName);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
            Compare(x => x.Role);
            Compare(x => x.Email);
            Compare(x => x.Validity);
        }
    }
}

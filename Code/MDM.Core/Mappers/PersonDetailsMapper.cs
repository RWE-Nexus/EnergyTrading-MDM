namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class PersonDetailsMapper : Mapper<EnergyTrading.MDM.PersonDetails, RWEST.Nexus.MDM.Contracts.PersonDetails>
    {
        public override void Map(EnergyTrading.MDM.PersonDetails source, RWEST.Nexus.MDM.Contracts.PersonDetails destination)
        {
            destination.Forename = source.FirstName;
            destination.Surname = source.LastName;
            destination.TelephoneNumber = source.Phone;
            destination.FaxNumber = source.Fax;
            destination.Role = source.Role;
            destination.Email = source.Email;
        }
    }
}
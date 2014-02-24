namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    public class PartyDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.PartyDetails, MDM.PartyDetails>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.PartyDetails source, MDM.PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.Phone = source.TelephoneNumber;
            destination.Fax = source.FaxNumber;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}

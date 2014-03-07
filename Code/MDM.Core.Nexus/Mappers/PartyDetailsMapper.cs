namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class PartyDetailsMapper : Mapper<EnergyTrading.MDM.PartyDetails, RWEST.Nexus.MDM.Contracts.PartyDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyDetails source, RWEST.Nexus.MDM.Contracts.PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.TelephoneNumber = source.Phone;
            destination.FaxNumber = source.Fax;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}
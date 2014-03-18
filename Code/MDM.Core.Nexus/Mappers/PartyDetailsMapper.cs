namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class PartyDetailsMapper : Mapper<EnergyTrading.MDM.PartyDetails, OpenNexus.MDM.Contracts.PartyDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyDetails source, OpenNexus.MDM.Contracts.PartyDetails destination)
        {
            destination.Name = source.Name;
            destination.TelephoneNumber = source.Phone;
            destination.FaxNumber = source.Fax;
            destination.Role = source.Role;
            destination.IsInternal = source.IsInternal;
        }
    }
}
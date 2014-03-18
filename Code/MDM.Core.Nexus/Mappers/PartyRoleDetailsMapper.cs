namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleDetailsMapper : Mapper<EnergyTrading.MDM.PartyRoleDetails, OpenNexus.MDM.Contracts.PartyRoleDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyRoleDetails source, OpenNexus.MDM.Contracts.PartyRoleDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}

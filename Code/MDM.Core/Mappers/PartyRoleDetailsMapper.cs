namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleDetailsMapper : Mapper<EnergyTrading.MDM.PartyRoleDetails, RWEST.Nexus.MDM.Contracts.PartyRoleDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyRoleDetails source, RWEST.Nexus.MDM.Contracts.PartyRoleDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}

namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="PartyRoleAccountability" /> to a <see cref="RWEST.Nexus.MDM.Contracts.PartyRoleAccountabilityDetails" />
    /// </summary>
    public class PartyRoleAccountabilityDetailsMapper : Mapper<EnergyTrading.MDM.PartyRoleAccountability, OpenNexus.MDM.Contracts.PartyRoleAccountabilityDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyRoleAccountability source, OpenNexus.MDM.Contracts.PartyRoleAccountabilityDetails destination)
        {
            destination.Name = source.Name;
            destination.SourcePartyRole = source.SourcePartyRole.CreateNexusEntityId(() => source.SourcePartyRole.LatestDetails.Name);
            destination.TargetPartyRole = source.TargetPartyRole.CreateNexusEntityId(() => source.TargetPartyRole.LatestDetails.Name);
            destination.PartyRoleAccountabilityType = source.PartyRoleAccountabilityType;
        }
    }
}
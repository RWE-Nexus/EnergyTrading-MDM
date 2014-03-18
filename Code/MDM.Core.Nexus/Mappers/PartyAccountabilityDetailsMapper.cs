namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="PartyAccountability" /> to a <see cref="RWEST.Nexus.MDM.Contracts.PartyAccountabilityDetails" />
    /// </summary>
    public class PartyAccountabilityDetailsMapper : Mapper<EnergyTrading.MDM.PartyAccountability, OpenNexus.MDM.Contracts.PartyAccountabilityDetails>
    {
        public override void Map(EnergyTrading.MDM.PartyAccountability source, OpenNexus.MDM.Contracts.PartyAccountabilityDetails destination)
        {
            destination.Name = source.Name;
            destination.SourceParty = source.SourceParty.CreateNexusEntityId(() => source.SourceParty.LatestDetails.Name);
            destination.TargetParty = source.TargetParty.CreateNexusEntityId(() => source.TargetParty.LatestDetails.Name);
            destination.SourcePerson = source.SourcePerson.CreateNexusEntityId(() => source.SourcePerson.LatestDetails.FirstName + " " + source.SourcePerson.LatestDetails.LastName);
            destination.TargetPerson = source.TargetPerson.CreateNexusEntityId(() => source.TargetPerson.LatestDetails.FirstName + " " + source.TargetPerson.LatestDetails.LastName);
            destination.PartyAccountabilityType = source.PartyAccountabilityType;
        }
    }
}
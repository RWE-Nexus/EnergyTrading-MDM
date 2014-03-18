namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PartyRoleMapper : Mapper<EnergyTrading.MDM.PartyRole, OpenNexus.MDM.Contracts.PartyRole>
    {
        public override void Map(EnergyTrading.MDM.PartyRole source, OpenNexus.MDM.Contracts.PartyRole destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
            destination.PartyRoleType = source.PartyRoleType;
        }
    }
}
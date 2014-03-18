namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class SettlementContactMapper : Mapper<EnergyTrading.MDM.SettlementContact, OpenNexus.MDM.Contracts.SettlementContact>
    {
        public override void Map(EnergyTrading.MDM.SettlementContact source, OpenNexus.MDM.Contracts.SettlementContact destination)
        {
            destination.Details.SourceParty = source.SourceParty.CreateNexusEntityId(() => source.SourceParty.LatestDetails.Name);
            destination.Details.TargetParty = source.TargetParty.CreateNexusEntityId(() => source.TargetParty.LatestDetails.Name);
            destination.Details.SourcePerson = source.SourcePerson.CreateNexusEntityId(() => source.SourcePerson.LatestDetails.FirstName + " " + source.SourcePerson.LatestDetails.LastName);
            destination.Details.TargetPerson = source.TargetPerson.CreateNexusEntityId(() => source.TargetPerson.LatestDetails.FirstName + " " + source.TargetPerson.LatestDetails.LastName);
        }
    }
}


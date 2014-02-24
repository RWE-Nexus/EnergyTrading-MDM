namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class LegalEntityMapper : Mapper<EnergyTrading.MDM.LegalEntity, RWEST.Nexus.MDM.Contracts.LegalEntity>
    {
        public override void Map(EnergyTrading.MDM.LegalEntity source, RWEST.Nexus.MDM.Contracts.LegalEntity destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}

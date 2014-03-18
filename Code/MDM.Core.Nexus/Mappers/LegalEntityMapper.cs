namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class LegalEntityMapper : Mapper<EnergyTrading.MDM.LegalEntity, OpenNexus.MDM.Contracts.LegalEntity>
    {
        public override void Map(EnergyTrading.MDM.LegalEntity source, OpenNexus.MDM.Contracts.LegalEntity destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}

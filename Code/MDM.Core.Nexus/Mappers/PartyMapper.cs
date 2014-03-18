namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PartyMapper : Mapper<EnergyTrading.MDM.Party, OpenNexus.MDM.Contracts.Party>
    {
        public override void Map(EnergyTrading.MDM.Party source, OpenNexus.MDM.Contracts.Party destination)
        {
        }
    }
}

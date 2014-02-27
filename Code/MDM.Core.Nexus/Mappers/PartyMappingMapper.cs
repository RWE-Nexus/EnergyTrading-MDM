namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;

    public class PartyMappingMapper : Mapper<EnergyTrading.MDM.PartyMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PartyMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PartyMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
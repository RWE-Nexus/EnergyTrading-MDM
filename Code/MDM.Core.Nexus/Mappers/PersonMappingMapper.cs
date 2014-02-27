namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;

    public class PersonMappingMapper : Mapper<EnergyTrading.MDM.PersonMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PersonMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PersonMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
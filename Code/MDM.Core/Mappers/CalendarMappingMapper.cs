namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class CalendarMappingMapper: Mapper<EnergyTrading.MDM.CalendarMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public CalendarMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.CalendarMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
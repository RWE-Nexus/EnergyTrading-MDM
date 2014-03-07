namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class BookDefaultMappingMapper: Mapper<EnergyTrading.MDM.BookDefaultMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public BookDefaultMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.BookDefaultMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class BookMappingMapper: Mapper<EnergyTrading.MDM.BookMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public BookMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.BookMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
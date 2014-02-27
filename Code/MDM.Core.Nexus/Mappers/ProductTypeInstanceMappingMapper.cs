namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ProductTypeInstanceMappingMapper: Mapper<EnergyTrading.MDM.ProductTypeInstanceMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ProductTypeInstanceMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ProductTypeInstanceMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
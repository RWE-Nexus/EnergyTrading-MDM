namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ProductTypeMappingMapper: Mapper<EnergyTrading.MDM.ProductTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ProductTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ProductTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ProductMappingMapper: Mapper<EnergyTrading.MDM.ProductMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ProductMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ProductMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
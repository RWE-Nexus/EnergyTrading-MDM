namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ProductTenorTypeMappingMapper: Mapper<EnergyTrading.MDM.ProductTenorTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ProductTenorTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ProductTenorTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
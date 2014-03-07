namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ProductScotaMappingMapper: Mapper<EnergyTrading.MDM.ProductScotaMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ProductScotaMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ProductScotaMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
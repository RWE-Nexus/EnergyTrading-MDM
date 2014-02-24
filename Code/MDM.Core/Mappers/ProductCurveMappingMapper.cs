namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ProductCurveMappingMapper: Mapper<EnergyTrading.MDM.ProductCurveMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ProductCurveMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ProductCurveMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
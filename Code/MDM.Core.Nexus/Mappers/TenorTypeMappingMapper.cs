namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class TenorTypeMappingMapper: Mapper<EnergyTrading.MDM.TenorTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public TenorTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.TenorTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
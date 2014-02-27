namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class TenorMappingMapper: Mapper<EnergyTrading.MDM.TenorMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public TenorMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.TenorMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
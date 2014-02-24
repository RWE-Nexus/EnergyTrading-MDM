namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class UnitMappingMapper: Mapper<EnergyTrading.MDM.UnitMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public UnitMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.UnitMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class HierarchyMappingMapper: Mapper<EnergyTrading.MDM.HierarchyMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public HierarchyMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.HierarchyMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
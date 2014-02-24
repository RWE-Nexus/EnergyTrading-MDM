namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class PortfolioHierarchyMappingMapper: Mapper<EnergyTrading.MDM.PortfolioHierarchyMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PortfolioHierarchyMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PortfolioHierarchyMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
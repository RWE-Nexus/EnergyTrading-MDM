namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class PortfolioMappingMapper: Mapper<EnergyTrading.MDM.PortfolioMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public PortfolioMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PortfolioMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
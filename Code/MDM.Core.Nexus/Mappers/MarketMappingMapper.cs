namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class MarketMappingMapper: Mapper<EnergyTrading.MDM.MarketMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public MarketMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.MarketMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
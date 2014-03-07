namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class BrokerRateMappingMapper: Mapper<EnergyTrading.MDM.BrokerRateMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public BrokerRateMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.BrokerRateMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
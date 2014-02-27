namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ShipperCodeMappingMapper: Mapper<EnergyTrading.MDM.ShipperCodeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public ShipperCodeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ShipperCodeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
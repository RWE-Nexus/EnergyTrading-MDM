namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class VesselMappingMapper: Mapper<EnergyTrading.MDM.VesselMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public VesselMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.VesselMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
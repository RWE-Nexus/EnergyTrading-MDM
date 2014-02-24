namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class CurveMappingMapper: Mapper<EnergyTrading.MDM.CurveMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public CurveMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.CurveMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class FeeTypeMappingMapper: Mapper<EnergyTrading.MDM.FeeTypeMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public FeeTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.FeeTypeMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
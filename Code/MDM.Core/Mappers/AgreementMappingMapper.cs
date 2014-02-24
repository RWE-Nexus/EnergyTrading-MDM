namespace EnergyTrading.MDM.Mappers
{
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class AgreementMappingMapper: Mapper<EnergyTrading.MDM.AgreementMapping, NexusId>
    {
        private readonly Mapper<IEntityMapping, NexusId> mapper;

        public AgreementMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.AgreementMapping source, NexusId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mapping;

    public class SourceSystemMappingMapper: Mapper<EnergyTrading.MDM.SourceSystemMapping, MdmId>
    {
        private readonly Mapper<IEntityMapping, MdmId> mapper;

        public SourceSystemMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.SourceSystemMapping source, MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
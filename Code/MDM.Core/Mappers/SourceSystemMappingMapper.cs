namespace EnergyTrading.Mdm.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.Mdm.Contracts;

    public class SourceSystemMappingMapper: Mapper<EnergyTrading.Mdm.SourceSystemMapping, MdmId>
    {
        private readonly Mapper<IEntityMapping, MdmId> mapper;

        public SourceSystemMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.Mdm.SourceSystemMapping source, MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
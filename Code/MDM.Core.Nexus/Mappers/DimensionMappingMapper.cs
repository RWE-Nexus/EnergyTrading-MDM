namespace EnergyTrading.MDM.Mappers
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class DimensionMappingMapper: Mapper<EnergyTrading.MDM.DimensionMapping, EnergyTrading.Mdm.Contracts.MdmId>
    {
        private readonly Mapper<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId> mapper;

        public DimensionMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.DimensionMapping source, EnergyTrading.Mdm.Contracts.MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
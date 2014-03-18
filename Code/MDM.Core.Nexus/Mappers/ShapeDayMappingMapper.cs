namespace EnergyTrading.MDM.Mappers
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class ShapeDayMappingMapper: Mapper<EnergyTrading.MDM.ShapeDayMapping, EnergyTrading.Mdm.Contracts.MdmId>
    {
        private readonly Mapper<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId> mapper;

        public ShapeDayMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.ShapeDayMapping source, EnergyTrading.Mdm.Contracts.MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
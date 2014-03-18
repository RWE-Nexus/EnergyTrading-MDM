namespace EnergyTrading.MDM.Mappers
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class InstrumentTypeMappingMapper: Mapper<InstrumentTypeMapping, EnergyTrading.Mdm.Contracts.MdmId>
    {
        private readonly Mapper<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId> mapper;

        public InstrumentTypeMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.InstrumentTypeMapping source, EnergyTrading.Mdm.Contracts.MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
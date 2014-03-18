namespace EnergyTrading.MDM.Mappers
{
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;

    public class BookMappingMapper: Mapper<EnergyTrading.MDM.BookMapping, EnergyTrading.Mdm.Contracts.MdmId>
    {
        private readonly Mapper<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId> mapper;

        public BookMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.BookMapping source, EnergyTrading.Mdm.Contracts.MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
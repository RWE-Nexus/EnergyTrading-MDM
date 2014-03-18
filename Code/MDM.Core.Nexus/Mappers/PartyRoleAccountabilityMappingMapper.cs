namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using OpenNexus.MDM.Contracts;

    public class PartyRoleAccountabilityMappingMapper : Mapper<EnergyTrading.MDM.PartyRoleAccountabilityMapping, EnergyTrading.Mdm.Contracts.MdmId>
    {
        private readonly Mapper<IEntityMapping, EnergyTrading.Mdm.Contracts.MdmId> mapper;

        public PartyRoleAccountabilityMappingMapper()
        {
            this.mapper = new EntityMappingMapper();
        }

        public override void Map(EnergyTrading.MDM.PartyRoleAccountabilityMapping source, EnergyTrading.Mdm.Contracts.MdmId destination)
        {
            this.mapper.Map(source, destination);
        }
    }
}
namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class PartyRoleMapper : ContractMapper<PartyRole, MDM.PartyRole, PartyRoleDetails, MDM.PartyRoleDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public PartyRoleMapper(IMappingEngine mappingEngine, IRepository repository) : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.PartyRole source, MDM.PartyRole destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = source.PartyRoleType;
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
        }

        protected override PartyRoleDetails ContractDetails(PartyRole contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(PartyRole contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(PartyRole contract)
        {
            return contract.Identifiers;
        }
    }
}

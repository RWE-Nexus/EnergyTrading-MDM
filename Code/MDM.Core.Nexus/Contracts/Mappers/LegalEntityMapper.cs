namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="LegalEntity" />
    /// </summary>
    public class LegalEntityMapper : ContractMapper<LegalEntity, MDM.LegalEntity, LegalEntityDetails, MDM.LegalEntityDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public LegalEntityMapper(IRepository repository, IMappingEngine mappingEngine) : base(mappingEngine)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.LegalEntity source, MDM.LegalEntity destination)
        {
            base.Map(source, destination);

            destination.PartyRoleType = "Legal Entity";
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
        }

        protected override LegalEntityDetails ContractDetails(LegalEntity contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(LegalEntity contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(LegalEntity contract)
        {
            return contract.Identifiers;
        }
    }
}
namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Data;
    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Exchange" />
    /// </summary>
    public class ExchangeMapper : ContractMapper<Exchange, MDM.Exchange, ExchangeDetails, MDM.ExchangeDetails, PartyRoleMapping>
    {
        private readonly IRepository repository;

        public ExchangeMapper(IMappingEngine mappingEngine, IRepository repository) : base(mappingEngine)
        {
            this.repository = repository;
        }

        protected override ExchangeDetails ContractDetails(Exchange contract)
        {
            return contract.Details;
        }

        public override void Map(OpenNexus.MDM.Contracts.Exchange source, MDM.Exchange destination)
        {
            base.Map(source, destination);
            destination.PartyRoleType = "Exchange";
            destination.Party = this.repository.FindEntityByMapping<MDM.Party, PartyMapping>(source.Party);
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Exchange contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Exchange contract)
        {
            return contract.Identifiers;
        }
    }
}
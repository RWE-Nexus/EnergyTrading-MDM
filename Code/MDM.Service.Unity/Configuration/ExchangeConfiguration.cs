namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;

    public class ExchangeConfiguration : EntityConfiguration<Services.ExchangeService, MDM.Exchange, RWEST.Nexus.MDM.Contracts.Exchange, 
		PartyRoleMapping, ExchangeValidator>
    {
        public ExchangeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "exchange"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Exchange, MDM.Exchange>, EnergyTrading.MDM.Contracts.Mappers.ExchangeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ExchangeDetails, MDM.ExchangeDetails>, EnergyTrading.MDM.Contracts.Mappers.ExchangeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ExchangeDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Exchange, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Exchange, RWEST.Nexus.MDM.Contracts.Exchange>, MDM.Mappers.ExchangeMapper>();
        }
    }
}
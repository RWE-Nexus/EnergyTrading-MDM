namespace MDM.ServiceHost.Unity.Nexus.Configuration
{
    using System.Collections.Generic;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;
    using EnergyTrading.MDM.Services;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;

    using Exchange = EnergyTrading.MDM.Exchange;
    using ExchangeDetails = EnergyTrading.MDM.ExchangeDetails;

    public class ExchangeConfiguration : NexusEntityConfiguration<ExchangeService, Exchange, RWEST.Nexus.MDM.Contracts.Exchange, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Exchange, Exchange>, EnergyTrading.MDM.Contracts.Mappers.ExchangeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ExchangeDetails, ExchangeDetails>, EnergyTrading.MDM.Contracts.Mappers.ExchangeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ExchangeDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Exchange, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Exchange, RWEST.Nexus.MDM.Contracts.Exchange>, EnergyTrading.MDM.Mappers.ExchangeMapper>();
        }
    }
}
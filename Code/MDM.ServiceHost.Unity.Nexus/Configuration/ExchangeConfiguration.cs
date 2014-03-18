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

    using EnergyTrading.Contracts.Atom;

    using Exchange = EnergyTrading.MDM.Exchange;
    using ExchangeDetails = EnergyTrading.MDM.ExchangeDetails;

    public class ExchangeConfiguration : NexusEntityConfiguration<ExchangeService, Exchange, OpenNexus.MDM.Contracts.Exchange, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Exchange, Exchange>, EnergyTrading.MDM.Contracts.Mappers.ExchangeMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.ExchangeDetails, ExchangeDetails>, EnergyTrading.MDM.Contracts.Mappers.ExchangeDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ExchangeDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<Exchange, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Exchange, OpenNexus.MDM.Contracts.Exchange>, EnergyTrading.MDM.Mappers.ExchangeMapper>();
        }
    }
}
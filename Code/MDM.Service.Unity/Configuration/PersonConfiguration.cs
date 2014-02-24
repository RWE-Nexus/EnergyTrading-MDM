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

    public class PersonConfiguration : EntityConfiguration<Services.PersonService, MDM.Person, RWEST.Nexus.MDM.Contracts.Person, PersonMapping, PersonValidator>
    {
        public PersonConfiguration(IUnityContainer container) : base(container)
        {

        }

        protected override string Name
        {
            get { return "person"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Person, MDM.Person>, EnergyTrading.MDM.Contracts.Mappers.PersonMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PersonDetails, MDM.PersonDetails>, EnergyTrading.MDM.Contracts.Mappers.PersonDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PersonMapping>, MappingMapper<PersonMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.PersonDetailsMapper());
            MappingEngine.RegisterMap(new PersonMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Person, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Person, RWEST.Nexus.MDM.Contracts.Person>, MDM.Mappers.PersonMapper>();
        }
    }
}
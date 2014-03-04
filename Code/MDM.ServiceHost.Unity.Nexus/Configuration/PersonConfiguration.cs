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

    using Person = EnergyTrading.MDM.Person;
    using PersonDetails = EnergyTrading.MDM.PersonDetails;

    public class PersonConfiguration : NexusEntityConfiguration<PersonService, Person, RWEST.Nexus.MDM.Contracts.Person, PersonMapping, PersonValidator>
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Person, Person>, EnergyTrading.MDM.Contracts.Mappers.PersonMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.PersonDetails, PersonDetails>, EnergyTrading.MDM.Contracts.Mappers.PersonDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PersonMapping>, MappingMapper<PersonMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.PersonDetailsMapper());
            this.MappingEngine.RegisterMap(new PersonMappingMapper());      
            this.Container.RegisterType<IMapper<Person, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Person, RWEST.Nexus.MDM.Contracts.Person>, EnergyTrading.MDM.Mappers.PersonMapper>();
        }
    }
}
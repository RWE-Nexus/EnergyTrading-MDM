namespace EnergyTrading.MDM.Configuration
{
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;

    using RWEST.Nexus.Contracts.Atom;
    using EnergyTrading.Mapping;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.MDM.Mappers;
    

    public class BookDefaultConfiguration : EntityConfiguration<Services.BookDefaultService, MDM.BookDefault, RWEST.Nexus.MDM.Contracts.BookDefault, 
        BookDefaultMapping, BookDefaultValidator>
    {
        public BookDefaultConfiguration(IUnityContainer container)
            : base(container)
        {
        }

        protected override string Name
        {
            get { return "bookdefault"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BookDefault, MDM.BookDefault>, EnergyTrading.MDM.Contracts.Mappers.BookDefaultMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BookDefaultDetails, MDM.BookDefault>, EnergyTrading.MDM.Contracts.Mappers.BookDefaultDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, BookDefaultMapping>, MappingMapper<BookDefaultMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.BookDefaultDetailsMapper());
            MappingEngine.RegisterMap(new BookDefaultMappingMapper());
            this.Container.RegisterType<IMapper<MDM.BookDefault, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.BookDefault, RWEST.Nexus.MDM.Contracts.BookDefault>, MDM.Mappers.BookDefaultMapper>();
        }
    }
}
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

    using BookDefault = EnergyTrading.MDM.BookDefault;

    public class BookDefaultConfiguration : NexusEntityConfiguration<BookDefaultService, BookDefault, OpenNexus.MDM.Contracts.BookDefault, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.BookDefault, BookDefault>, EnergyTrading.MDM.Contracts.Mappers.BookDefaultMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.BookDefaultDetails, BookDefault>, EnergyTrading.MDM.Contracts.Mappers.BookDefaultDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, BookDefaultMapping>, MappingMapper<BookDefaultMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.BookDefaultDetailsMapper());
            this.MappingEngine.RegisterMap(new BookDefaultMappingMapper());
            this.Container.RegisterType<IMapper<BookDefault, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<BookDefault, OpenNexus.MDM.Contracts.BookDefault>, EnergyTrading.MDM.Mappers.BookDefaultMapper>();
        }
    }
}
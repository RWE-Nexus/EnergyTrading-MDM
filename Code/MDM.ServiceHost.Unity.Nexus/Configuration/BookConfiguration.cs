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

    using Book = EnergyTrading.MDM.Book;

    public class BookConfiguration : NexusEntityConfiguration<BookService, Book, OpenNexus.MDM.Contracts.Book, 
        BookMapping, BookValidator>
    {
        public BookConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "book"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Book, Book>, EnergyTrading.MDM.Contracts.Mappers.BookMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.BookDetails, Book>, EnergyTrading.MDM.Contracts.Mappers.BookDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, BookMapping>, MappingMapper<BookMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.BookDetailsMapper());
            this.MappingEngine.RegisterMap(new BookMappingMapper());      
            this.Container.RegisterType<IMapper<Book, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Book, OpenNexus.MDM.Contracts.Book>, EnergyTrading.MDM.Mappers.BookMapper>();
        }
    }
}
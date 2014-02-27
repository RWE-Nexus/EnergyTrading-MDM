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
    

    public class BookConfiguration : NexusEntityConfiguration<Services.BookService, MDM.Book, RWEST.Nexus.MDM.Contracts.Book, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Book, MDM.Book>, EnergyTrading.MDM.Contracts.Mappers.BookMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BookDetails, MDM.Book>, EnergyTrading.MDM.Contracts.Mappers.BookDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, BookMapping>, MappingMapper<BookMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.BookDetailsMapper());
            MappingEngine.RegisterMap(new BookMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Book, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Book, RWEST.Nexus.MDM.Contracts.Book>, MDM.Mappers.BookMapper>();
        }
    }
}
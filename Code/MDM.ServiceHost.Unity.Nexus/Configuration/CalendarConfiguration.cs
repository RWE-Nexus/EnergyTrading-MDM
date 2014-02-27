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

    public class CalendarConfiguration : NexusEntityConfiguration<Services.CalendarService, MDM.Calendar, RWEST.Nexus.MDM.Contracts.Calendar, 
		CalendarMapping, CalendarValidator>
    {
        public CalendarConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "calendar"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Calendar, MDM.Calendar>, EnergyTrading.MDM.Contracts.Mappers.CalendarMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.CalendarDetails, MDM.Calendar>, EnergyTrading.MDM.Contracts.Mappers.CalendarDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, CalendarMapping>, MappingMapper<CalendarMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.CalendarDetailsMapper());
            MappingEngine.RegisterMap(new CalendarMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Calendar, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Calendar, RWEST.Nexus.MDM.Contracts.Calendar>, MDM.Mappers.CalendarMapper>();
        }
    }
}
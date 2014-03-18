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

    using Calendar = EnergyTrading.MDM.Calendar;

    public class CalendarConfiguration : NexusEntityConfiguration<CalendarService, Calendar, OpenNexus.MDM.Contracts.Calendar, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Calendar, Calendar>, EnergyTrading.MDM.Contracts.Mappers.CalendarMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.CalendarDetails, Calendar>, EnergyTrading.MDM.Contracts.Mappers.CalendarDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, CalendarMapping>, MappingMapper<CalendarMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.CalendarDetailsMapper());
            this.MappingEngine.RegisterMap(new CalendarMappingMapper());      
            this.Container.RegisterType<IMapper<Calendar, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Calendar, OpenNexus.MDM.Contracts.Calendar>, EnergyTrading.MDM.Mappers.CalendarMapper>();
        }
    }
}
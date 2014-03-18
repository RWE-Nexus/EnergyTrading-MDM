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

    using Unit = EnergyTrading.MDM.Unit;

    public class UnitConfiguration : NexusEntityConfiguration<UnitService, Unit, OpenNexus.MDM.Contracts.Unit, 
		UnitMapping, UnitValidator>
    {
        public UnitConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "unit"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Unit, Unit>, EnergyTrading.MDM.Contracts.Mappers.UnitMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.UnitDetails, Unit>, EnergyTrading.MDM.Contracts.Mappers.UnitDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, UnitMapping>, MappingMapper<UnitMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.UnitDetailsMapper());
            this.MappingEngine.RegisterMap(new UnitMappingMapper());      
            this.Container.RegisterType<IMapper<Unit, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Unit, OpenNexus.MDM.Contracts.Unit>, EnergyTrading.MDM.Mappers.UnitMapper>();
        }
    }
}
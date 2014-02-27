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

    public class UnitConfiguration : NexusEntityConfiguration<Services.UnitService, MDM.Unit, RWEST.Nexus.MDM.Contracts.Unit, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Unit, MDM.Unit>, EnergyTrading.MDM.Contracts.Mappers.UnitMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.UnitDetails, MDM.Unit>, EnergyTrading.MDM.Contracts.Mappers.UnitDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, UnitMapping>, MappingMapper<UnitMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.UnitDetailsMapper());
            MappingEngine.RegisterMap(new UnitMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Unit, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Unit, RWEST.Nexus.MDM.Contracts.Unit>, MDM.Mappers.UnitMapper>();
        }
    }
}
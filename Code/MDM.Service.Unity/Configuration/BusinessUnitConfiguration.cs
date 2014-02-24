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

    public class BusinessUnitConfiguration : EntityConfiguration<Services.BusinessUnitService, MDM.BusinessUnit, RWEST.Nexus.MDM.Contracts.BusinessUnit, 
		PartyRoleMapping, BusinessUnitValidator>
    {
        public BusinessUnitConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "businessunit"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BusinessUnit, MDM.BusinessUnit>, EnergyTrading.MDM.Contracts.Mappers.BusinessUnitMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.BusinessUnitDetails, MDM.BusinessUnitDetails>, EnergyTrading.MDM.Contracts.Mappers.BusinessUnitDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.BusinessUnitDetailsMapper());
            MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.BusinessUnit, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.BusinessUnit, RWEST.Nexus.MDM.Contracts.BusinessUnit>, MDM.Mappers.BusinessUnitMapper>();
        }
    }
}
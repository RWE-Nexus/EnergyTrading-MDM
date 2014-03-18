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

    using BusinessUnit = EnergyTrading.MDM.BusinessUnit;
    using BusinessUnitDetails = EnergyTrading.MDM.BusinessUnitDetails;

    public class BusinessUnitConfiguration : NexusEntityConfiguration<BusinessUnitService, BusinessUnit, OpenNexus.MDM.Contracts.BusinessUnit, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.BusinessUnit, BusinessUnit>, EnergyTrading.MDM.Contracts.Mappers.BusinessUnitMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.BusinessUnitDetails, BusinessUnitDetails>, EnergyTrading.MDM.Contracts.Mappers.BusinessUnitDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, PartyRoleMapping>, MappingMapper<PartyRoleMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.BusinessUnitDetailsMapper());
            this.MappingEngine.RegisterMap(new PartyRoleMappingMapper());      
            this.Container.RegisterType<IMapper<BusinessUnit, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<BusinessUnit, OpenNexus.MDM.Contracts.BusinessUnit>, EnergyTrading.MDM.Mappers.BusinessUnitMapper>();
        }
    }
}
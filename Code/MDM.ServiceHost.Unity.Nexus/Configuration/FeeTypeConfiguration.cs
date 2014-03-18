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

    using FeeType = EnergyTrading.MDM.FeeType;

    public class FeeTypeConfiguration : NexusEntityConfiguration<FeeTypeService, FeeType, OpenNexus.MDM.Contracts.FeeType, 
		FeeTypeMapping, FeeTypeValidator>
    {
        public FeeTypeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "feetype"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.FeeType, FeeType>, EnergyTrading.MDM.Contracts.Mappers.FeeTypeMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.FeeTypeDetails, FeeType>, EnergyTrading.MDM.Contracts.Mappers.FeeTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, FeeTypeMapping>, MappingMapper<FeeTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.FeeTypeDetailsMapper());
            this.MappingEngine.RegisterMap(new FeeTypeMappingMapper());      
            this.Container.RegisterType<IMapper<FeeType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<FeeType, OpenNexus.MDM.Contracts.FeeType>, EnergyTrading.MDM.Mappers.FeeTypeMapper>();
        }
    }
}
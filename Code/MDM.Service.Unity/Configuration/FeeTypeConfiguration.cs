namespace EnergyTrading.MDM.Configuration
{
    using Microsoft.Practices.Unity;
	using System.Collections.Generic;

    using RWEST.Nexus.Contracts.Atom;
    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Contracts.Mappers;
    using EnergyTrading.MDM.Contracts.Validators;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Mappers;
	

    public class FeeTypeConfiguration : EntityConfiguration<Services.FeeTypeService, MDM.FeeType, RWEST.Nexus.MDM.Contracts.FeeType, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.FeeType, MDM.FeeType>, EnergyTrading.MDM.Contracts.Mappers.FeeTypeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.FeeTypeDetails, MDM.FeeType>, EnergyTrading.MDM.Contracts.Mappers.FeeTypeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, FeeTypeMapping>, MappingMapper<FeeTypeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.FeeTypeDetailsMapper());
            MappingEngine.RegisterMap(new FeeTypeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.FeeType, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.FeeType, RWEST.Nexus.MDM.Contracts.FeeType>, MDM.Mappers.FeeTypeMapper>();
        }
    }
}
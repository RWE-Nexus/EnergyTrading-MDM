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

    public class DimensionConfiguration : EntityConfiguration<Services.DimensionService, MDM.Dimension, RWEST.Nexus.MDM.Contracts.Dimension, 
		DimensionMapping, DimensionValidator>
    {
        public DimensionConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "dimension"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.Dimension, MDM.Dimension>, EnergyTrading.MDM.Contracts.Mappers.DimensionMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.DimensionDetails, MDM.Dimension>, EnergyTrading.MDM.Contracts.Mappers.DimensionDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, DimensionMapping>, MappingMapper<DimensionMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.DimensionDetailsMapper());
            MappingEngine.RegisterMap(new DimensionMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.Dimension, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.Dimension, RWEST.Nexus.MDM.Contracts.Dimension>, MDM.Mappers.DimensionMapper>();
        }
    }
}
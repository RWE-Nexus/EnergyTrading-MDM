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

    using Dimension = EnergyTrading.MDM.Dimension;

    public class DimensionConfiguration : NexusEntityConfiguration<DimensionService, Dimension, OpenNexus.MDM.Contracts.Dimension, 
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
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.Dimension, Dimension>, EnergyTrading.MDM.Contracts.Mappers.DimensionMapper>();
            this.Container.RegisterType<IMapper<OpenNexus.MDM.Contracts.DimensionDetails, Dimension>, EnergyTrading.MDM.Contracts.Mappers.DimensionDetailsMapper>();
            this.Container.RegisterType<IMapper<EnergyTrading.Mdm.Contracts.MdmId, DimensionMapping>, MappingMapper<DimensionMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.DimensionDetailsMapper());
            this.MappingEngine.RegisterMap(new DimensionMappingMapper());      
            this.Container.RegisterType<IMapper<Dimension, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<Dimension, OpenNexus.MDM.Contracts.Dimension>, EnergyTrading.MDM.Mappers.DimensionMapper>();
        }
    }
}
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

    using RWEST.Nexus.Contracts.Atom;

    using ShipperCode = EnergyTrading.MDM.ShipperCode;

    public class ShipperCodeConfiguration : NexusEntityConfiguration<ShipperCodeService, ShipperCode, RWEST.Nexus.MDM.Contracts.ShipperCode, 
		ShipperCodeMapping, ShipperCodeValidator>
    {
        public ShipperCodeConfiguration(IUnityContainer container) : base(container)
        {
        }

        protected override string Name
        {
            get { return "shippercode"; }
        }

        protected override void ContractDomainMapping()
        {
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShipperCode, ShipperCode>, EnergyTrading.MDM.Contracts.Mappers.ShipperCodeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShipperCodeDetails, ShipperCode>, EnergyTrading.MDM.Contracts.Mappers.ShipperCodeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShipperCodeMapping>, MappingMapper<ShipperCodeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            this.MappingEngine.RegisterMap(new EnergyTrading.MDM.Mappers.ShipperCodeDetailsMapper());
            this.MappingEngine.RegisterMap(new ShipperCodeMappingMapper());      
            this.Container.RegisterType<IMapper<ShipperCode, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<ShipperCode, RWEST.Nexus.MDM.Contracts.ShipperCode>, EnergyTrading.MDM.Mappers.ShipperCodeMapper>();
        }
    }
}
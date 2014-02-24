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

    public class ShipperCodeConfiguration : EntityConfiguration<Services.ShipperCodeService, MDM.ShipperCode, RWEST.Nexus.MDM.Contracts.ShipperCode, 
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
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShipperCode, MDM.ShipperCode>, EnergyTrading.MDM.Contracts.Mappers.ShipperCodeMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.ShipperCodeDetails, MDM.ShipperCode>, EnergyTrading.MDM.Contracts.Mappers.ShipperCodeDetailsMapper>();
            this.Container.RegisterType<IMapper<RWEST.Nexus.MDM.Contracts.NexusId, ShipperCodeMapping>, MappingMapper<ShipperCodeMapping>>();
        }

        protected override void DomainContractMapping()
        {
            MappingEngine.RegisterMap(new Mappers.ShipperCodeDetailsMapper());
            MappingEngine.RegisterMap(new ShipperCodeMappingMapper());      
            this.Container.RegisterType<IMapper<MDM.ShipperCode, List<Link>>, NullLinksMapper>();
            this.Container.RegisterType<IMapper<MDM.ShipperCode, RWEST.Nexus.MDM.Contracts.ShipperCode>, MDM.Mappers.ShipperCodeMapper>();
        }
    }
}
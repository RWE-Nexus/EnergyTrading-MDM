namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ProductScota" />
    /// </summary>
    public class ProductScotaMapper : ContractMapper<ProductScota, MDM.ProductScota, ProductScotaDetails, MDM.ProductScota, ProductScotaMapping>
    {
        public ProductScotaMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ProductScotaDetails ContractDetails(ProductScota contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ProductScota contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(ProductScota contract)
        {
            return contract.Identifiers;
        }
    }
}
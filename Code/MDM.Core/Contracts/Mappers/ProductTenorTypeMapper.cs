namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ProductTenorType" />
    /// </summary>
    public class ProductTenorTypeMapper : ContractMapper<ProductTenorType, MDM.ProductTenorType, ProductTenorTypeDetails, MDM.ProductTenorType, ProductTenorTypeMapping>
    {
        public ProductTenorTypeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ProductTenorTypeDetails ContractDetails(ProductTenorType contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ProductTenorType contract)
        {
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(ProductTenorType contract)
        {
            return contract.Identifiers;
        }
    }
}
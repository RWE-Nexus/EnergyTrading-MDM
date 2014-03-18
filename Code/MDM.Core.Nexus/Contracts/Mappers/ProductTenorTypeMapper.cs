namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(ProductTenorType contract)
        {
            return contract.Identifiers;
        }
    }
}
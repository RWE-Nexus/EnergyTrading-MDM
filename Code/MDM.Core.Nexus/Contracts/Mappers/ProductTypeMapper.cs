namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ProductType" />
    /// </summary>
    public class ProductTypeMapper : ContractMapper<ProductType, MDM.ProductType, ProductTypeDetails, MDM.ProductType, ProductTypeMapping>
    {
        public ProductTypeMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ProductTypeDetails ContractDetails(ProductType contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ProductType contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(ProductType contract)
        {
            return contract.Identifiers;
        }
    }
}
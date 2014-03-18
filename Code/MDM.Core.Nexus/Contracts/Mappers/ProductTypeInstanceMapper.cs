namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ProductTypeInstance" />
    /// </summary>
    public class ProductTypeInstanceMapper : ContractMapper<ProductTypeInstance, MDM.ProductTypeInstance, ProductTypeInstanceDetails, MDM.ProductTypeInstance, ProductTypeInstanceMapping>
    {
        public ProductTypeInstanceMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ProductTypeInstanceDetails ContractDetails(ProductTypeInstance contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ProductTypeInstance contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(ProductTypeInstance contract)
        {
            return contract.Identifiers;
        }
    }
}
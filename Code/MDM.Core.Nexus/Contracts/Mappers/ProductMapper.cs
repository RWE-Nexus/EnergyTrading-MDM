namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="Product" />
    /// </summary>
    public class ProductMapper : ContractMapper<Product, MDM.Product, ProductDetails, MDM.Product, ProductMapping>
    {
        public ProductMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ProductDetails ContractDetails(Product contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(Product contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(Product contract)
        {
            return contract.Identifiers;
        }
    }
}
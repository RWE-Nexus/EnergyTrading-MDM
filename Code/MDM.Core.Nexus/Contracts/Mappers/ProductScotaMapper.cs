namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(ProductScota contract)
        {
            return contract.Identifiers;
        }
    }
}
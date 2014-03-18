namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using OpenNexus.MDM.Contracts;
    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="SourceSystem" /> to a <see cref="ProductCurve" />
    /// </summary>
    public class ProductCurveMapper : ContractMapper<ProductCurve, MDM.ProductCurve, ProductCurveDetails, MDM.ProductCurve, ProductCurveMapping>
    {
        public ProductCurveMapper(IMappingEngine mappingEngine) : base(mappingEngine)
        {
        }

        protected override ProductCurveDetails ContractDetails(ProductCurve contract)
        {
            return contract.Details;
        }

        protected override EnergyTrading.DateRange ContractDetailsValidity(ProductCurve contract)
        {
            return this.SystemDataValidity(contract.MdmSystemData);
        }

        protected override IEnumerable<EnergyTrading.Mdm.Contracts.MdmId> Identifiers(ProductCurve contract)
        {
            return contract.Identifiers;
        }
    }
}
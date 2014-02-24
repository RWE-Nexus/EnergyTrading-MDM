namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System.Collections.Generic;

    using RWEST.Nexus.MDM.Contracts;
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
            return this.SystemDataValidity(contract.Nexus);
        }

        protected override IEnumerable<RWEST.Nexus.MDM.Contracts.NexusId> Identifiers(ProductCurve contract)
        {
            return contract.Identifiers;
        }
    }
}
using EnergyTrading.Data;
using EnergyTrading.MDM.Data;

namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class ProductCurveDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.ProductCurveDetails, MDM.ProductCurve>
    {
        private readonly IRepository repository;

        public ProductCurveDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(RWEST.Nexus.MDM.Contracts.ProductCurveDetails source, MDM.ProductCurve destination)
        {
            destination.Name = source.Name;
            destination.Curve = this.repository.FindEntityByMapping<MDM.Curve, CurveMapping>(source.Curve);
            destination.Product = this.repository.FindEntityByMapping<MDM.Product, ProductMapping>(source.Product);
            destination.ProductCurveType = source.ProductCurveType;
            destination.ProjectionMethod = source.ProjectionMethod;
        }
    }
}
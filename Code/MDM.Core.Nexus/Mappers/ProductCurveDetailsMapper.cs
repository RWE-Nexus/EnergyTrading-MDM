using EnergyTrading.MDM.Extensions;

namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="ProductCurve" /> to a <see cref="RWEST.Nexus.MDM.Contracts.ProductCurveDetails" />
    /// </summary>
    public class ProductCurveDetailsMapper : Mapper<EnergyTrading.MDM.ProductCurve, OpenNexus.MDM.Contracts.ProductCurveDetails>
    {
        public override void Map(EnergyTrading.MDM.ProductCurve source, OpenNexus.MDM.Contracts.ProductCurveDetails destination)
        {
            destination.Name = source.Name;
            destination.Curve = source.Curve.CreateNexusEntityId(() => source.Curve.Name);
            destination.Product = source.Product.CreateNexusEntityId(() => source.Product.Name);
            destination.ProductCurveType = source.ProductCurveType;
            destination.ProjectionMethod = source.ProjectionMethod;
        }
    }
}
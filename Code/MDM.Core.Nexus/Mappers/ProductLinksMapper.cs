namespace EnergyTrading.MDM.Mappers
{
    using System.Collections.Generic;

    using EnergyTrading.Contracts.Atom;
    using EnergyTrading.Mapping;

    using Product = EnergyTrading.MDM.Product;

    public class ProductLinksMapper : Mapper<EnergyTrading.MDM.Product, List<Link>>
    {
        public override void Map(EnergyTrading.MDM.Product source, List<Link> destination)
        {
            foreach (var productCurve in source.ProductCurves)
            {
                destination.Add(new Link
                    { 
                        Rel = "get-related-productcurve", 
                        Type = "ProductCurve", 
                        Uri = string.Format("/ProductCurve/{0}", productCurve.Id)
                    });
            }
        }
    }
}

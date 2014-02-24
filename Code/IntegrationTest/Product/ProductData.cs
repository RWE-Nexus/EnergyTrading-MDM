namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using Product = EnergyTrading.MDM.Product;

    public partial class ProductData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Product contract)
        {
            var product = ObjectMother.Create<Product>();
            this.repository.Add(product);
            this.repository.Flush();

            contract.Details = new ProductDetails()
                {
                    CalendarRule = product.CalendarRule,
                    Market = product.Market.CreateNexusEntityId(() => product.Market.Name),
                    Shape = product.Shape.CreateNexusEntityId(() => product.Shape.Name),
                    CommodityInstrumentType = product.CommodityInstrumentType.CreateNexusEntityId(() =>
                    string.Format(
                        "{0}|{1}|{2}",
                        product.CommodityInstrumentType.Commodity == null ? string.Empty : product.CommodityInstrumentType.Commodity.Name,
                        product.CommodityInstrumentType.InstrumentType == null ? string.Empty : product.CommodityInstrumentType.InstrumentType.Name,
                        product.CommodityInstrumentType.InstrumentDelivery)),
                    DefaultCurve = product.DefaultCurve.CreateNexusEntityId(() => product.DefaultCurve.Name),
                    Name = Guid.NewGuid().ToString(),
                    LotSize = 5
                };
        }

        partial void AddDetailsToEntity(MDM.Product entity, DateTime startDate, DateTime endDate)
        {
            var product = ObjectMother.Create<Product>();
            entity.Market = product.Market;
            entity.Shape = product.Shape;
            entity.CommodityInstrumentType = product.CommodityInstrumentType;
            entity.CalendarRule = product.CalendarRule;
            entity.DefaultCurve = product.DefaultCurve;
            entity.Name = Guid.NewGuid().ToString();
            entity.LotSize = 3;
        }

        partial void CreateSearchData(Search search, MDM.Product entity1, MDM.Product entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}

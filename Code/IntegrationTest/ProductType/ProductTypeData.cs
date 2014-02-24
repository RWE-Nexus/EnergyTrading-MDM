namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using ProductType = EnergyTrading.MDM.ProductType;

    public partial class ProductTypeData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ProductType contract)
        {
            var productType = ObjectMother.Create<ProductType>();
            this.repository.Add(productType);
            this.repository.Flush();

            contract.Details = new ProductTypeDetails()
                {
                    DeliveryRangeType = "test",
                    Name = Guid.NewGuid().ToString(),
                    Product = productType.Product.CreateNexusEntityId(() => productType.Product.Name)
                };
        }

        partial void AddDetailsToEntity(MDM.ProductType entity, DateTime startDate, DateTime endDate)
        {
            var productType = ObjectMother.Create<ProductType>();
            this.repository.Add(productType);
            this.repository.Flush();

            entity.Product = productType.Product;
            entity.Name = Guid.NewGuid().ToString();
            entity.DeliveryRangeType = "test";
        }

        partial void CreateSearchData(Search search, MDM.ProductType entity1, MDM.ProductType entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}

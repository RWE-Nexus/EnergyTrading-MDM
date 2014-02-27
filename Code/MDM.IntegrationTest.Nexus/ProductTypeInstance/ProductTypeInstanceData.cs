namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading.Contracts.Search;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Search;

    using Product = EnergyTrading.MDM.Product;
    using ProductTypeInstance = EnergyTrading.MDM.ProductTypeInstance;

    public partial class ProductTypeInstanceData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.ProductTypeInstance contract)
        {
            var productTypeInstance = ObjectMother.Create<ProductTypeInstance>();
            this.repository.Add(productTypeInstance);
            this.repository.Flush();

            contract.Details = new ProductTypeInstanceDetails()
                {
                    Delivery = new DateRange() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2) },
                    ProductType = productTypeInstance.ProductType.CreateNexusEntityId(() => productTypeInstance.ProductType.Name), Name = Guid.NewGuid().ToString()
                };
        }

        partial void AddDetailsToEntity(MDM.ProductTypeInstance entity, DateTime startDate, DateTime endDate)
        {
            var productTypeInstance = ObjectMother.Create<ProductTypeInstance>();
            this.repository.Add(productTypeInstance);
            this.repository.Flush();

            entity.Delivery = productTypeInstance.Delivery;
            entity.Name = Guid.NewGuid().ToString();
            entity.ProductType = productTypeInstance.ProductType;
        }

        partial void CreateSearchData(Search search, MDM.ProductTypeInstance entity1, MDM.ProductTypeInstance entity2)
        {
            search.AddSearchCriteria(SearchCombinator.Or)
                .AddCriteria("Id", SearchCondition.NumericEquals, entity1.Id.ToString())
                .AddCriteria("Id", SearchCondition.NumericEquals, entity2.Id.ToString());
        }
    }
}

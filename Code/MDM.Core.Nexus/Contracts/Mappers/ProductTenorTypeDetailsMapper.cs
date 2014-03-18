namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.MDM.Data;
    using EnergyTrading.Mapping;

    public class ProductTenorTypeDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ProductTenorTypeDetails, MDM.ProductTenorType>
    {
        private readonly IRepository repository;

        public ProductTenorTypeDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ProductTenorTypeDetails source, MDM.ProductTenorType destination)
        {
            destination.Product = this.repository.FindEntityByMapping<MDM.Product, ProductMapping>(source.Product);
            destination.TenorType = this.repository.FindEntityByMapping<MDM.TenorType, TenorTypeMapping>(source.TenorType);
        }
    }
}
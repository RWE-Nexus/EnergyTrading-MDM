namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class ProductScotaDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ProductScotaDetails, MDM.ProductScota>
    {
        private readonly IRepository repository;

        public ProductScotaDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.ProductScotaDetails source, MDM.ProductScota destination)
        {
            destination.Name = source.Name;
            destination.Product = this.repository.FindEntityByMapping<MDM.Product, ProductMapping>(source.Product);
            destination.ScotaDeliveryPoint = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.ScotaDeliveryPoint);
            destination.ScotaOrigin = this.repository.FindEntityByMapping<MDM.Location, LocationMapping>(source.ScotaOrigin);
            destination.ScotaContract = source.ScotaContract;
            destination.ScotaRss = source.ScotaRss;
            destination.ScotaVersion = source.ScotaVersion;
        }
    }
}
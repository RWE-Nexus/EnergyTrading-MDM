namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Data;
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Data;

    public class CommodityDetailsMapper : Mapper<OpenNexus.MDM.Contracts.CommodityDetails, MDM.Commodity>
    {
        private readonly IRepository repository;

        public CommodityDetailsMapper(IRepository repository)
        {
            this.repository = repository;
        }

        public override void Map(OpenNexus.MDM.Contracts.CommodityDetails source, MDM.Commodity destination)
        {
            destination.Name = source.Name;
            destination.MassEnergyValue = source.MassEnergyValue;
            destination.VolumeEnergyValue = source.VolumeEnergyValue;
            destination.VolumetricDensityValue = source.VolumetricDensityValue;
            destination.DeliveryRate = source.DeliveryRate;
            destination.Parent = this.repository.FindEntityByMapping<MDM.Commodity, CommodityMapping>(source.Parent);
            destination.MassEnergyUnits = this.repository.FindEntityByMapping<MDM.Unit, UnitMapping>(source.MassEnergyUnits);
            destination.VolumeEnergyUnits = this.repository.FindEntityByMapping<MDM.Unit, UnitMapping>(source.VolumeEnergyUnits);
            destination.VolumetricDensityUnits = this.repository.FindEntityByMapping<MDM.Unit, UnitMapping>(source.VolumetricDensityUnits);
        }
    }
}
namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class CommodityDetailsMapper : Mapper<EnergyTrading.MDM.Commodity, RWEST.Nexus.MDM.Contracts.CommodityDetails>
    {
        public override void Map(EnergyTrading.MDM.Commodity source, RWEST.Nexus.MDM.Contracts.CommodityDetails destination)
        {
            destination.Name = source.Name;
            destination.MassEnergyValue = source.MassEnergyValue;
            destination.VolumeEnergyValue = source.VolumeEnergyValue;
            destination.VolumetricDensityValue = source.VolumetricDensityValue;
            destination.DeliveryRate = source.DeliveryRate;
            destination.Parent = source.Parent.CreateNexusEntityId(() => source.Parent.Name);
            destination.MassEnergyUnits = source.MassEnergyUnits.CreateNexusEntityId(() => source.MassEnergyUnits.Name);
            destination.VolumeEnergyUnits = source.VolumeEnergyUnits.CreateNexusEntityId(() => source.VolumeEnergyUnits.Name);
            destination.VolumetricDensityUnits = source.VolumetricDensityUnits.CreateNexusEntityId(() => source.VolumetricDensityUnits.Name);
        }
    }
}
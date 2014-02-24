namespace EnergyTrading.MDM
{
    public partial class Commodity
    {
        public string Name { get; set; }

        public virtual Commodity Parent { get; set; }

        public decimal? MassEnergyValue { get; set; }

        public virtual Unit MassEnergyUnits { get; set; }

        public decimal? VolumeEnergyValue { get; set; }

        public virtual Unit VolumeEnergyUnits { get; set; }

        public decimal? VolumetricDensityValue { get; set; }

        public virtual Unit VolumetricDensityUnits { get; set; }

        public string DeliveryRate { get; set; }

        partial void CopyDetails(Commodity details)
        {
            // force the load of related entities to make sure that updating these to null deletes the relationship in EF
            var forceLoadOfParent = this.Parent;
            var forceLoadOfMassEnergyUnits = this.MassEnergyUnits;
            var forceLoadOfVolumeEnergyUnits = this.VolumeEnergyUnits;
            var forceLoadOfVolumetricDensityUnits = this.VolumetricDensityUnits;

            this.Name = details.Name;
            this.Parent = details.Parent;
            this.MassEnergyValue = details.MassEnergyValue;
            this.MassEnergyUnits = details.MassEnergyUnits;
            this.VolumeEnergyValue = details.VolumeEnergyValue;
            this.VolumeEnergyUnits = details.VolumeEnergyUnits;
            this.VolumetricDensityValue = details.VolumetricDensityValue;
            this.VolumetricDensityUnits = details.VolumetricDensityUnits;
            this.DeliveryRate = details.DeliveryRate;
        }
    }
}
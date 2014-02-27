namespace EnergyTrading.MDM
{
    using System;

    public partial class Dimension
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int LengthDimension { get; set; }

        public int MassDimension { get; set; }

        public int TimeDimension { get; set; }

        public int ElectricCurrentDimension { get; set; }

        public int TemperatureDimension { get; set; }

        public int LuminousIntensityDimension { get; set; }

        partial void CopyDetails(Dimension details)
        {
            this.Name = details.Name;
            this.Description = details.Description;
            this.LengthDimension = details.LengthDimension;
            this.MassDimension = details.MassDimension;
            this.TimeDimension = details.TimeDimension;
            this.ElectricCurrentDimension = details.ElectricCurrentDimension;
            this.TemperatureDimension = details.TemperatureDimension;
            this.LuminousIntensityDimension = details.LuminousIntensityDimension;
        }
    }
}

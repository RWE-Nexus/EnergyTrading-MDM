namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class FeeTypeDetailsMapper : Mapper<OpenNexus.MDM.Contracts.FeeTypeDetails, MDM.FeeType>
    {
        public override void Map(OpenNexus.MDM.Contracts.FeeTypeDetails source, MDM.FeeType destination)
        {
            destination.Name = source.Name;
        }
    }
}
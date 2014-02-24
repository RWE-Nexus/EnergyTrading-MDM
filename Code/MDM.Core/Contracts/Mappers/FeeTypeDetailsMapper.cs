namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class FeeTypeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.FeeTypeDetails, MDM.FeeType>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.FeeTypeDetails source, MDM.FeeType destination)
        {
            destination.Name = source.Name;
        }
    }
}
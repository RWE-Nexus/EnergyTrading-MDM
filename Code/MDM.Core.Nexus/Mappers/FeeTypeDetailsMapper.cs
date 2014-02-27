namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="FeeType" /> to a <see cref="RWEST.Nexus.MDM.Contracts.FeeTypeDetails" />
    /// </summary>
    public class FeeTypeDetailsMapper : Mapper<EnergyTrading.MDM.FeeType, RWEST.Nexus.MDM.Contracts.FeeTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.FeeType source, RWEST.Nexus.MDM.Contracts.FeeTypeDetails destination)
        {
            // TODO_CodeGeneration_FeeType

            destination.Name = source.Name;
        }
    }
}
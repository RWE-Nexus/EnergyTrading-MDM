namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
	
    /// <summary>
    /// Maps a <see cref="Hierarchy" /> to a <see cref="RWEST.Nexus.MDM.Contracts.HierarchyDetails" />
    /// </summary>
    public class HierarchyDetailsMapper : Mapper<EnergyTrading.MDM.Hierarchy, OpenNexus.MDM.Contracts.HierarchyDetails>
    {
        public override void Map(EnergyTrading.MDM.Hierarchy source, OpenNexus.MDM.Contracts.HierarchyDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}
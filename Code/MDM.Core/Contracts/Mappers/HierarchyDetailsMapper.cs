namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class HierarchyDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.HierarchyDetails, MDM.Hierarchy>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.HierarchyDetails source, MDM.Hierarchy destination)
        {
            destination.Name = source.Name;
        }
    }
}
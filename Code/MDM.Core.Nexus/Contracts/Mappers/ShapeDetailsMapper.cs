namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class ShapeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.ShapeDetails, MDM.Shape>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.ShapeDetails source, MDM.Shape destination)
        {
            destination.Name = source.Name;
        }
    }
}
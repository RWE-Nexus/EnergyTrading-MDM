namespace EnergyTrading.MDM.Contracts.Mappers
{
    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class ShapeDetailsMapper : Mapper<OpenNexus.MDM.Contracts.ShapeDetails, MDM.Shape>
    {
        public override void Map(OpenNexus.MDM.Contracts.ShapeDetails source, MDM.Shape destination)
        {
            destination.Name = source.Name;
        }
    }
}
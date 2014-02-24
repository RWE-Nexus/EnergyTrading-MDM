namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    /// <summary>
	///
	/// </summary>
    public class BookDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.BookDetails, MDM.Book>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.BookDetails source, MDM.Book destination)
        {
            destination.Name = source.Name;
        }
    }
}
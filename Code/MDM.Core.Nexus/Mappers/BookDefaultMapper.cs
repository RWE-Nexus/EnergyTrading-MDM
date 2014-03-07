namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class BookDefaultMapper : Mapper<EnergyTrading.MDM.BookDefault, RWEST.Nexus.MDM.Contracts.BookDefault>
    {
        public override void Map(EnergyTrading.MDM.BookDefault source, RWEST.Nexus.MDM.Contracts.BookDefault destination)
        {
        }
    }
}

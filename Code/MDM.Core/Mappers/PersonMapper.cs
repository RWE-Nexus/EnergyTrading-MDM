namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PersonMapper : Mapper<EnergyTrading.MDM.Person, RWEST.Nexus.MDM.Contracts.Person>
    {
        public override void Map(EnergyTrading.MDM.Person source, RWEST.Nexus.MDM.Contracts.Person destination)
        {
        }
    }
}

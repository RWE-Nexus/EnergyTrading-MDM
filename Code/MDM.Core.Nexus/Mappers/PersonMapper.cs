namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    public class PersonMapper : Mapper<EnergyTrading.MDM.Person, OpenNexus.MDM.Contracts.Person>
    {
        public override void Map(EnergyTrading.MDM.Person source, OpenNexus.MDM.Contracts.Person destination)
        {
        }
    }
}

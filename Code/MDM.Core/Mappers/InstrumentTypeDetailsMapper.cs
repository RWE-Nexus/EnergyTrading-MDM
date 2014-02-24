namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class InstrumentTypeDetailsMapper : Mapper<EnergyTrading.MDM.InstrumentType, RWEST.Nexus.MDM.Contracts.InstrumentTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.InstrumentType source, RWEST.Nexus.MDM.Contracts.InstrumentTypeDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}
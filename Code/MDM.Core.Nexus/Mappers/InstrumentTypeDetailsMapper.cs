namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.Mapping;

    public class InstrumentTypeDetailsMapper : Mapper<EnergyTrading.MDM.InstrumentType, OpenNexus.MDM.Contracts.InstrumentTypeDetails>
    {
        public override void Map(EnergyTrading.MDM.InstrumentType source, OpenNexus.MDM.Contracts.InstrumentTypeDetails destination)
        {
            destination.Name = source.Name;
        }
    }
}
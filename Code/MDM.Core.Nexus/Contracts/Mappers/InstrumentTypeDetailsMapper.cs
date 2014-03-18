namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    public class InstrumentTypeDetailsMapper : Mapper<OpenNexus.MDM.Contracts.InstrumentTypeDetails, MDM.InstrumentType>
    {
        public override void Map(OpenNexus.MDM.Contracts.InstrumentTypeDetails source, MDM.InstrumentType destination)
        {
            destination.Name = source.Name;
        }
    }
}

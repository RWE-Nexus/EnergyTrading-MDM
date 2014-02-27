namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;

    using EnergyTrading.Mapping;

    public class InstrumentTypeDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.InstrumentTypeDetails, MDM.InstrumentType>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.InstrumentTypeDetails source, MDM.InstrumentType destination)
        {
            destination.Name = source.Name;
        }
    }
}

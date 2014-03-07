namespace EnergyTrading.MDM.Mappers
{
    using EnergyTrading.MDM.Extensions;
    using EnergyTrading.Mapping;

    public class BusinessUnitMapper : Mapper<EnergyTrading.MDM.BusinessUnit, RWEST.Nexus.MDM.Contracts.BusinessUnit>
    {
        public override void Map(EnergyTrading.MDM.BusinessUnit source, RWEST.Nexus.MDM.Contracts.BusinessUnit destination)
        {
            destination.Party = source.Party.CreateNexusEntityId(() => source.Party.LatestDetails.Name);
        }
    }
}
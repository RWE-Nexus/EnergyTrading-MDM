namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="BusinessUnit" /> to a <see cref="RWEST.Nexus.MDM.Contracts.BusinessUnitDetails" />
    /// </summary>
    public class BusinessUnitDetailsMapper : Mapper<EnergyTrading.MDM.BusinessUnitDetails, RWEST.Nexus.MDM.Contracts.BusinessUnitDetails>
    {
        public override void Map(EnergyTrading.MDM.BusinessUnitDetails source, RWEST.Nexus.MDM.Contracts.BusinessUnitDetails destination)
        {
            destination.Name = source.Name;
            destination.Fax = source.Fax;
            destination.Phone = source.Phone;
            destination.AccountType = source.AccountType;
            destination.Address = source.Address;
            destination.Status = source.Status;
            destination.TaxLocation = source.TaxLocation.CreateNexusEntityId(() => source.TaxLocation.Name);
        }
    }
}
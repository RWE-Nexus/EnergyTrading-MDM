namespace EnergyTrading.MDM.Mappers
{
    using System;

    using EnergyTrading.Mapping;
    using EnergyTrading.MDM.Extensions;

    /// <summary>
    /// Maps a <see cref="Agreement" /> to a <see cref="RWEST.Nexus.MDM.Contracts.AgreementDetails" />
    /// </summary>
    public class AgreementDetailsMapper : Mapper<EnergyTrading.MDM.Agreement, RWEST.Nexus.MDM.Contracts.AgreementDetails>
    {
        public override void Map(EnergyTrading.MDM.Agreement source, RWEST.Nexus.MDM.Contracts.AgreementDetails destination)
        {
            destination.Name = source.Name;
            destination.PaymentTerms = source.PaymentTerms;
        }
    }
}
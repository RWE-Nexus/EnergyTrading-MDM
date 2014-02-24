using EnergyTrading.Data;

namespace EnergyTrading.MDM.Contracts.Mappers
{
    using System;
    using EnergyTrading.Mapping;
    public class AgreementDetailsMapper : Mapper<RWEST.Nexus.MDM.Contracts.AgreementDetails, MDM.Agreement>
    {
        public override void Map(RWEST.Nexus.MDM.Contracts.AgreementDetails source, MDM.Agreement destination)
        {
            destination.Name = source.Name;
            destination.PaymentTerms = source.PaymentTerms;
        }
    }
}
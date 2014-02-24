namespace EnergyTrading.MDM.Test
{
    using System;

    using EnergyTrading;

    public partial class PartyData
    {
        partial void AddDetailsToContract(RWEST.Nexus.MDM.Contracts.Party contract)
        {
            contract.Details = new RWEST.Nexus.MDM.Contracts.PartyDetails() { Name = Guid.NewGuid().ToString() };
        }

        partial void AddDetailsToEntity(MDM.Party entity, DateTime startDate, DateTime endDate)
        {
            entity.AddDetails(
                new PartyDetails()
                    { Name = Guid.NewGuid().ToString(), Validity = new DateRange(startDate, endDate) });
        }
    }
}

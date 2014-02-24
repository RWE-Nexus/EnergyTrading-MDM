namespace EnergyTrading.MDM.Data
{
    using EnergyTrading;
    using RWEST.Nexus.MDM;

    public interface IRanged
    {
        DateRange Validity { get; set; }
    }
}
namespace EnergyTrading.MDM.Data
{
    using EnergyTrading;
    using EnergyTrading.Mdm;

    public interface IRanged
    {
        DateRange Validity { get; set; }
    }
}
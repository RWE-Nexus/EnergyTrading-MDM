namespace EnergyTrading.MDM
{
    /// <summary>
    /// Details for an MDM entity.
    /// </summary>
    public interface IEntityDetail : IRangedChild
    {
        int Id { get; }
    }
}
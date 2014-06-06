namespace EnergyTrading.Mdm
{
    /// <summary>
    /// Details for an MDM entity.
    /// </summary>
    public interface IEntityDetail : IRangedChild
    {
        /// <summary>
        /// Gets the id.
        /// <para>
        /// In the case of non-temporal entities this will be same as for the entity.
        /// For temporal entities, there will be one IEntityDetail per temporal slice with its
        /// own identity.
        /// </para>
        /// </summary>
        int Id { get; }
    }
}
namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;

    /// <summary>
    /// Type of a location role.
    /// </summary>
    public class LocationRoleType : IIdentifiable
    {
        public int Id { get; set; }

        object IIdentifiable.Id
        {
            get { return Id; }
        }

        public string Name { get; set; }
    }
}
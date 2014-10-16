using EnergyTrading.Data;

namespace EnergyTrading.Mdm
{
    /// <summary>
    /// A key/value structure for holding general reference / lookup data
    /// </summary>
    public class ReferenceData : IIdentifiable
    {
        object IIdentifiable.Id
        {
            get { return Id; }
        }

        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
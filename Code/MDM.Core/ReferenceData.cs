namespace EnergyTrading.MDM
{
    using EnergyTrading.Data;

    public class ReferenceData : IIdentifiable
    {
        object IIdentifiable.Id
        {
            get { return this.Id; }
        }

        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
namespace EnergyTrading.MDM
{
    using EnergyTrading;

    public class DeliveryInfo
    {
        /// <summary>
        /// Rule used to compute delivery
        /// </summary>
        public string DeliveryRule { get; set; }

        /// <summary>
        /// Rule use to compute the start of delivery
        /// </summary>
        public string DeliveryRuleStart { get; set; }

        /// <summary>
        /// Rule used to compute the finish of delivery
        /// </summary>
        public string DeliveryRuleFinish { get; set; }

        /// <summary>
        /// Specified delivery period
        /// </summary>
        public DateRange Delivery { get; set; }
    }
}
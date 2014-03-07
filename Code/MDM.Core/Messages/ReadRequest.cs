namespace EnergyTrading.MDM.Messages
{
    using System;

    using EnergyTrading;

    public class ReadRequest
    {
        private DateTime validAt;

        /// <summary>
        /// Gets or sets the ValidAt property.
        /// <para>
        /// The point in time used to restrict both source and target mappings.
        /// </para>
        /// </summary>
        public DateTime ValidAt 
        { 
            get
            {
                if (!ValidAtExists && validAt == DateTime.MinValue)
                {
                    validAt = SystemTime.UtcNow();
                }

                return validAt;
            }

            set
            {
                validAt = value;
                ValidAtExists = true;
            }
        }

        /// <summary>
        /// Whether the original request provides an explicit ValidAt
        /// </summary>
        public bool ValidAtExists { get; set; }

        /// <summary>
        /// Gets or sets the Version property.
        /// <para>
        /// The version of the mapping, used for caching
        /// </para>
        /// </summary>
        public ulong Version { get; set; }
    }
}

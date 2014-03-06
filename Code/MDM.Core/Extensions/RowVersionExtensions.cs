namespace EnergyTrading.MDM.Extensions
{
    using System;

    public static class RowVersionExtensions
    {
        public static ulong ToUnsignedLongVersion(this byte[] timestamp)
        {
            if (timestamp.Length != 8)
            {
                throw new ArgumentOutOfRangeException("timestamp", "must be an 8 byte array for ToVersion");
            }
            var newArray = new byte[8];
            Array.Copy(timestamp, newArray, 8);
            Array.Reverse(newArray);
            return BitConverter.ToUInt64(newArray, 0);
        }
    }
}
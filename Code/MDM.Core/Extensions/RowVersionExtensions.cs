namespace EnergyTrading.MDM.Extensions
{
    using System;

    public static class RowVersionExtensions
    {
        public static ulong ToUnsignedLongVersion(this byte[] timestamp)
        {
            if (timestamp.Length != 8)
            {
                throw new ArgumentOutOfRangeException("timestamp", "must be an 8 byte array for ToUnsignedLongVersion");
            }
            var newArray = new byte[8];
            Array.Copy(timestamp, newArray, 8); // copy so we don't alter the original timestamp supplied by the database
            // if we are little endian (normally on windows we are) reverse the bytes to get correct conversion
            if (BitConverter.IsLittleEndian) 
            {
                Array.Reverse(newArray);
            }

            return BitConverter.ToUInt64(newArray, 0);
        }
    }
}
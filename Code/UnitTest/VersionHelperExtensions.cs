using System;

namespace EnergyTrading.Mdm.Test
{
    public static class VersionHelperExtensions
    {
        public static byte[] GetVersionByteArray(this ulong version)
        {
            var array = BitConverter.GetBytes(version);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            return array;
        }
    }
}

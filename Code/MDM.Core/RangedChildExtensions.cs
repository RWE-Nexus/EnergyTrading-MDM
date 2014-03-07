namespace EnergyTrading.MDM
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RangedChildExtensions
    {
        public static ulong LatestVersion(this IEnumerable<IRangedChild> list)
        {
            return list.LatestVersion(ulong.MinValue);
        }

        public static ulong LatestVersion(this IEnumerable<IRangedChild> list, ulong version)
        {
            return list.Select(rangedChild => rangedChild.Version).Concat(new[] { version }).Max();
        }
    }
}
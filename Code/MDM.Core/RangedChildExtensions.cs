namespace EnergyTrading.MDM
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RangedChildExtensions
    {
        public static long LatestVersion(this IEnumerable<IRangedChild> list)
        {
            return list.LatestVersion(long.MinValue);
        }

        public static long LatestVersion(this IEnumerable<IRangedChild> list, long version)
        {
            return list.Select(rangedChild => rangedChild.Version).Concat(new[] { version }).Max();
        }
    }
}
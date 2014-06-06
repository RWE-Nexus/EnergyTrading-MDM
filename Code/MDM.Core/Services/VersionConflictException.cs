namespace EnergyTrading.Mdm.Services
{
    using System;

    /// <summary>
    /// Raised if an update conflicts with the latest version in the database
    /// </summary>
    public class VersionConflictException : Exception
    {
    }
}
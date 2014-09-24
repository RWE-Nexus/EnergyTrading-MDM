using System;
using EnergyTrading.Mdm.Contracts;

namespace EnergyTrading.Mdm.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    public enum Operation
    {
        Created,
        Modified,
        Deleted
    }

    /// <summary>
    /// Service to publish some form of notification on MDM entities / mappings being created / updated / deleted
    /// </summary>
    public interface IMdmNotificationService
    {
        /// <summary>
        /// Invoked by the MDM Service when the underlying entity or mapping has changed (create/update/delete)
        /// </summary>
        void Notify<TContract>(Func<TContract> contractDelegate, uint contractVersion, Operation operation) where TContract : class, IMdmEntity;
    }

    /// <summary>
    /// A default implementation which doesn't do anything
    /// </summary>
    public class DoNothingMdmNotificationService : IMdmNotificationService
    {
        public void Notify<TContract>(Func<TContract> contractDelegate, uint contractVersion, Operation operation) where TContract : class, IMdmEntity
        {
        }
    }
}
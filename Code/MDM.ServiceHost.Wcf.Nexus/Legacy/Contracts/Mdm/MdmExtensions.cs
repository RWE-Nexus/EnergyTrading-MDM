namespace RWEST.Nexus.MDM.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for handling MDM contracts.
    /// </summary>
    public static class MdmExtensions
    {
        /// <summary>
        /// Check whether an MDM entity has an identifier.
        /// </summary>
        /// <param name="entity">Entity to check</param>
        /// <param name="value">Identifier to use</param>
        /// <returns>true if the identifier is equal to any of the entities identifiers, otherwise false.</returns>
        public static bool HasIdentifier(this IMdmEntity entity, NexusId value)
        {
            if (entity == null || value == null)
            {
                return false;
            }

            return entity.Identifiers.Any(id => id.Equals(value));
        }

        /// <summary>
        /// Get the identity of the nexus identifier
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? NexusId(this EntityId value)
        {
            if (value == null || value.Identifier == null)
            {
                return null;
            }

            return value.Identifier.ToKey();
        }

        /// <summary>
        /// Determine the primary identifier for a MDM entity.
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <param name="systemName">System to use if no SourceSystemOriginated identifier exists</param>
        /// <returns>The identifier with SourceSystemOriginated, or the one identified by systemName or the first identifier</returns>
        public static NexusId PrimaryIdentifier(this IMdmEntity entity, string systemName = null)
        {
            return entity == null ? null : entity.Identifiers.PrimaryIdentifier(systemName);
        }

        /// <summary>
        /// Determine the primary identifier.
        /// </summary>
        /// <param name="identifiers">List of identifiers to query</param>
        /// <param name="systemName">System to use if no SourceSystemOriginated identifier exists</param>
        /// <returns>The identifier with SourceSystemOriginated, or the one identified by systemName or the first identifier</returns>
        public static NexusId PrimaryIdentifier(this IList<NexusId> identifiers, string systemName = null)
        {
            if (identifiers == null || identifiers.Count == 0)
            {
                return null;
            }

            var originatingIdentifier = identifiers.FirstOrDefault(tid => tid.SourceSystemOriginated);
            if (originatingIdentifier == null && !string.IsNullOrEmpty(systemName))
            {
                originatingIdentifier = identifiers.SystemId(systemName);
            }

            return originatingIdentifier ?? identifiers[0];
        }

        /// <summary>
        /// Gets a MdmId for a system
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <param name="systemName">System to check</param>
        /// <returns>First identifier found for the system or null if not found.</returns>
        public static NexusId SystemId(this IMdmEntity entity, string systemName = "Nexus")
        {
            return entity == null ? null : entity.Identifiers.SystemId(systemName);
        }

        /// <summary>
        /// Gets a MdmId for a system
        /// </summary>
        /// <param name="identifiers">Identifiers to use</param>
        /// <param name="systemName">System to check</param>
        /// <returns>First identifier found for the system or null if not found.</returns>
        public static NexusId SystemId(this IList<NexusId> identifiers, string systemName = "Nexus")
        {
            return identifiers == null ? null : identifiers.FirstOrDefault(x => x.SystemName == systemName);
        }

        /// <summary>
        /// Gets an identifier value for a system
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <param name="systemName">System to check</param>
        /// <returns>First identifier found for the system or <see cref="string.Empty"/> if not found.</returns>
        public static string SystemIdentifier(this IMdmEntity entity, string systemName = "Nexus")
        {
            var id = entity.SystemId(systemName);
            return id == null ? null : id.Identifier;
        }

        /// <summary>
        /// Gets an identifier value for a system
        /// </summary>
        /// <param name="identifiers">Identifiers to use</param>
        /// <param name="systemName">System to check</param>
        /// <returns>First identifier found for the system or <see cref="string.Empty"/> if not found.</returns>
        public static string SystemIdentifier(this IList<NexusId> identifiers, string systemName = "Nexus")
        {
            var id = identifiers.SystemId(systemName);
            return id == null ? null : id.Identifier;
        }

        /// <summary>
        /// Creates an <see cref="EntityId" /> from a <see cref="NexusId" />
        /// </summary>
        /// <param name="value">MdmId to use</param>
        /// <param name="name">Optional name to use</param>
        /// <returns>A new <see cref="EntityId" /> wrapping the original MdmId</returns>
        public static EntityId ToEntityId(this NexusId value, string name = null)
        {
            return new EntityId
            {
                Name = name,
                Identifier = value
            };
        }

        /// <summary>
        /// Convert a <see cref="EntityId" /> to a string identifier.
        /// </summary>
        /// <param name="value">EntityId to use.</param>
        /// <returns>Null if the identifier is null, value.Identifier otherwise.</returns>
        public static string ToIdentifier(this EntityId value)
        {
            return value == null ? null : value.Identifier.ToIdentifier();
        }

        /// <summary>
        /// Convert a <see cref="NexusId" /> to a string identifier.
        /// </summary>
        /// <param name="value">MdmId to use.</param>
        /// <returns>Null if the identifier is null, value.Identifier otherwise.</returns>
        public static string ToIdentifier(this NexusId value)
        {
            return value == null ? null : value.Identifier;
        }

        /// <summary>
        /// Convert the identifier to a numeric key.
        /// </summary>
        /// <param name="identifier">Identifier to use.</param>
        /// <param name="defaultKey">Value if not found, defaults to zero</param>
        /// <returns>Numeric value of the identifier or the default if the identifier is null or does not convert.</returns>
        public static int ToKey(this NexusId identifier, int defaultKey = 0)
        {
            if (identifier == null)
            {
                return defaultKey;
            }
            int key;
            return int.TryParse(identifier.Identifier, out key) ? key : defaultKey;
        }

        /// <summary>
        /// Converts an <see cref="EntityId" /> into a <see cref="Mapping" />
        /// </summary>
        /// <param name="value">EntityId to convert.</param>
        /// <returns>Converted mapping if not null, otherwise null</returns>
        public static Mapping ToMapping(this EntityId value)
        {
            return value == null ? null : ToMapping(value.Identifier);
        }

        /// <summary>
        /// Converts an <see cref="NexusId" /> into a <see cref="Mapping" />
        /// </summary>
        /// <param name="value">MdmId to convert.</param>
        /// <returns>Converted mapping if not null, otherwise null</returns>
        public static Mapping ToMapping(this NexusId value)
        {
            if (value == null)
            {
                return null;
            }

            // TODO: Do we want all the bits or just system/id, different overload?
            return new Mapping
            {
                MappingId = value.MappingId,
                SystemName = value.SystemName,
                Identifier = value.Identifier,
                IsNexusId = value.IsNexusId,
                DefaultReverseInd = value.DefaultReverseInd,
                SourceSystemOriginated = value.SourceSystemOriginated,
                StartDate = value.StartDate,
                EndDate = value.EndDate
            };
        }

        /// <summary>
        /// Locate the Nexus MDM identifier for a MDM entity.
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <returns></returns>
        public static NexusId ToMdmId(this IMdmEntity entity)
        {
            return entity == null ? null : entity.Identifiers.FirstOrDefault(id => id.IsNexusId);
        }

        /// <summary>
        /// Get the Nexus MDM key for a MDM entity
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <param name="defaultValue"></param>
        /// <returns>Value of the entity's MDM Id if present, default value (0) otherwise.</returns>
        public static int ToMdmKey(this IMdmEntity entity, int defaultValue = 0)
        {
            var nexusId = entity.ToMdmId();
            return nexusId.ToKey(defaultValue);
        }

        /// <summary>
        /// Get the Nexus MDM key for a property of a MDM entity.
        /// </summary>
        /// <typeparam name="T">Type of MDM entity</typeparam>
        /// <param name="entity">Entity to use</param>
        /// <param name="access">Function to acquire the property EntityId</param>
        /// <returns>Value of the property's entity if present/integer, 0 otherwise.</returns>
        public static int ToMdmKey<T>(this T entity, Func<T, EntityId> access)
            where T : class, IMdmEntity
        {
            if (entity == null || entity.Details == null)
            {
                return 0;
            }

            return access(entity).ToNexusKey();
        }

        /// <summary>
        /// Get the Nexus key for a MDM entity
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <returns></returns>
        public static string ToMdmKeyString(this IMdmEntity entity)
        {
            var nexusId = entity.ToMdmId();
            return nexusId.ToIdentifier();
        }

        /// <summary>
        /// Locate the Nexus identifier for a MDM entity.
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <returns></returns>
        [Obsolete("Use ToMdmId")]
        public static NexusId ToNexusId(this IMdmEntity entity)
        {
            return entity.Identifiers.FirstOrDefault(id => id.IsNexusId);
        }

        /// <summary>
        /// Get the Nexus key for a MDM entity
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [Obsolete("Use ToMdmKey")]
        public static int ToNexusKey(this IMdmEntity entity, int defaultValue = 0)
        {
            var nexusId = entity.ToNexusId();
            return nexusId.ToKey(defaultValue);
        }

        /// <summary>
        /// Get the entity identifier from an <see cref="EntityId" />
        /// </summary>
        /// <param name="id">Identifier to use.</param>
        /// <param name="defaultValue">Value if not found, defaults to zero</param>        
        /// <returns></returns>      
        public static int ToNexusKey(this EntityId id, int defaultValue = 0)
        {
            return id == null ? defaultValue : id.Identifier.ToKey();
        }

        /// <summary>
        /// Get the Nexus key for a MDM entity
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <returns></returns>
        [Obsolete("Use ToMdmKeyString")]        
        public static string ToNexusKeyString(this IMdmEntity entity)
        {
            var nexusId = entity.ToNexusId();
            return nexusId == null ? string.Empty : nexusId.Identifier;
        }

        /// <summary>
        /// Location the system identifier for a MDM entity.
        /// </summary>
        /// <param name="entity">Entity to use</param>
        /// <param name="systemName">Name of system to locate</param>
        /// <returns></returns>
        public static NexusId ToSystemId(this IMdmEntity entity, string systemName)
        {
            return entity == null ? null : entity.Identifiers.SystemId(systemName);
        }
    }
}
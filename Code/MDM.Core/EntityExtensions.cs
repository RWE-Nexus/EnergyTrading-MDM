namespace EnergyTrading.Mdm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading;
    using EnergyTrading.Mdm.Data;

    public static class EntityExtensions
    {
        /// <summary>
        /// When a new detail is submitted and the start date is the same as the latest detail start date then return true
        /// </summary>
        /// <typeparam name="TDetails"></typeparam>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static bool ShouldUpdateLatestDetail<TDetails>(this IEntity entity, IList<TDetails> list, TDetails details)
            where TDetails : class, IEntityDetail
        {
            CheckAddDetailsPreCondtions(entity, list, details);

            var latest = list.Latest();

            // Terminate the latest detail and add a new one - or update the latest)
            if (latest != null && details.Validity.Start == latest.Validity.Start)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Add a details to an entity ensuring that it is overlaps or is continguous with the latest details.
        /// </summary>
        /// <typeparam name="TDetails"></typeparam>
        /// <param name="entity"></param>
        /// <param name="list"></param>
        /// <param name="details"></param>
        public static void AddDetails<TDetails>(this IEntity entity, IList<TDetails> list, TDetails details)
            where TDetails : class, IEntityDetail
        {
            CheckAddDetailsPreCondtions(entity, list, details);

            var latest = list.Latest();

            if (latest != null)
            {
                // Terminate the existing one
                latest.Validity = latest.Validity.TerminateRange(details.Validity);
            }

            // Add the new one
            details.Entity = entity;
            list.Add(details);
        }

        public static void CheckAddDetailsPreCondtions<TDetails>(IEntity entity, IList<TDetails> list, TDetails details)
            where TDetails : class, IEntityDetail
        {
            // Sanity checks
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }

            // Find the latest
            var latest = list.Latest();

            // Need to check the date constraint)
            if (latest != null)
            {
                // Is the start ok, can't start before the latest record
                if (details.Validity.Start < latest.Validity.Start)
                {
                    throw new ArgumentOutOfRangeException(
                        "details", "Validity range starts on or before start of latest range");
                }

                if (details.Validity.Start > latest.Validity.Finish)
                {
                    throw new ArgumentOutOfRangeException("details", "Validity range not contiguous with latest range");
                }
            }
        }

        public static void TrimMappings<TMapping>(this IList<TMapping> list, DateTime latest)
            where TMapping : IEntityMapping
        {
            foreach (var mapping in list.Where(x => x.Validity.Finish > latest))
            {
                mapping.ChangeEndDate(latest);
            } 
        }

        public static void AddMapping<TMapping>(this IEntity entity, IList<TMapping> list, TMapping mapping, DateTime maxDate)
            where TMapping : IEntityMapping
        {
            // Sanity checks
            if (Equals(mapping, default(TMapping)))
            {
                throw new ArgumentNullException("mapping");
            }

            // Ensure we know who owns the mapping
            mapping.Entity = entity;

            if (maxDate < mapping.Validity.Finish)
            {
                // Trim back the range to the allowed maximum
                mapping.ChangeEndDate(maxDate);
            }

            // Find the latest mapping for this system/identifier
            var latest = list.Latest(mapping);

            // Never seen this one, so it must be valid)
            if (latest != null)
            {
                // Is the start ok, can't start before the latest record
                if (mapping.Validity.Start <= latest.Validity.Start)
                {
                    throw new ArgumentOutOfRangeException("mapping", "Validity range starts on or before start of latest range for same system/identifier");
                }

                // Terminate the existing one
                latest.Validity = latest.Validity.TerminateRange(mapping.Validity);
            }

            // Add the new one
            list.Add(mapping);
        }

        /// <summary>
        /// Are two mappings compatible at a basic level.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool CompatibleMapping(this IEntityMapping source, IEntityMapping candidate)
        {
            return source.MappingId == candidate.MappingId 
                && source.System == candidate.System 
                && source.MappingValue == candidate.MappingValue
                && source.IsMaster == candidate.IsMaster
                && source.IsDefault == candidate.IsDefault;          
        }

        /// <summary>
        /// Updates a mapping from another mapping
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target"></param>
        public static void UpdateMapping(this IEntityMapping value, IEntityMapping target)
        {
            if (!value.CompatibleMapping(target))
            {
                throw new ArgumentOutOfRangeException("value", "Mapping not compatible");
            }

            value.ChangeStartDate(target.Validity.Start);
            value.ChangeEndDate(target.Validity.Finish);
        }

        public static void ProcessMapping<TMapping>(this IEntity entity, IList<TMapping> list, TMapping mapping, DateTime maxDate)
            where TMapping : IEntityMapping
        {
            // Sanity checks
            if (mapping == null)
            {
                throw new ArgumentNullException("mapping");
            }

            if (mapping.MappingId == 0)
            {
                entity.AddMapping(list, mapping, maxDate);
                return;
            }

            var current = list.FirstOrDefault(x => x.MappingId == mapping.MappingId);
            if (current == null)
            {
                throw new ArgumentOutOfRangeException("mapping", string.Format("Entity {0}: Mapping Id {1} not found", entity.Id, mapping.Id));
            }

            current.UpdateMapping(mapping);
        }

        public static TDetails Latest<TDetails>(this IEnumerable<TDetails> list)
            where TDetails : IEntityDetail
        {
            return list.Latest(x => true);
        }

        public static TMapping Latest<TMapping>(this IEnumerable<TMapping> list, TMapping value)
            where TMapping : IEntityMapping
        {
            return list.Latest(x => x.System == value.System && x.MappingValue == value.MappingValue);
        }

        public static T Latest<T>(this IEnumerable<T> values, Func<T, bool> query)
            where T : IRanged
        {
            T latest = default(T);
            foreach (var candidate in values.Where(query))
            {
                if (latest == null || candidate.Validity.Finish > latest.Validity.Finish)
                {
                    latest = candidate;
                }
            }

            return latest;
        }

        private static DateRange TerminateRange(this DateRange range, DateRange newRange)
        {
            return range.ChangeFinish(newRange.Start.AddSeconds(-1));
        }
    }
}
﻿namespace EnergyTrading.Mdm.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnergyTrading.Data;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Messages;

    public static class RepositoryExtensions
    {
        public static int FindOverlappingMappingCount<TMapping>(this IRepository repository, string sourceSystem, string mapping, EnergyTrading.DateRange range)
            where TMapping : class, IEntityMapping
        {
            return repository.FindOverlappingMappingCount<TMapping>(sourceSystem, mapping, range, 0);
        }

        public static int FindOverlappingMappingCount<TMapping>(this IRepository repository, string sourceSystem, string mapping, EnergyTrading.DateRange range, int mappingId)
            where TMapping : class, IEntityMapping
        {
            return repository.FindOverlappingMappings<TMapping>(sourceSystem, mapping, range, mappingId).Count();
        }

        public static IQueryable<TMapping> FindOverlappingMappings<TMapping>(this IRepository repository, string sourceSystem, string mapping, EnergyTrading.DateRange range, int mappingId)
            where TMapping : class, IEntityMapping
        {
            var query = repository.Queryable<TMapping>()
                                  .Where(x => x.System.Name == sourceSystem
                                      && x.MappingValue == mapping
                                      // Explicit Overlaps as LINQ to Entity can't cope with interfaces
                                      && !(x.Validity.Start >= range.Finish
                                      || x.Validity.Finish <= range.Start));
            if (mappingId != 0)
            {
                query = query.Where(x => (int)x.Id != mappingId);
            }

            return query;
        }

        /// <summary>
        /// Locate a mapping based on a request.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <typeparam name="TMapping"></typeparam>
        /// <param name="repository"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static TMapping FindMapping<TMapping>(this IRepository repository, MappingRequest request)
            where TMapping : class, IEntityMapping
        {
            return repository.FindMapping<TMapping>(request.SystemName, request.Identifier, request.ValidAt);
        }

        public static TMapping FindMapping<TMapping>(this IRepository repository, string sourceSystem, string mapping, DateTime validAt)
            where TMapping : class, IEntityMapping
        {
            return repository.Queryable<TMapping>()
                            .Where(x => x.System.Name == sourceSystem
                                && x.MappingValue == mapping
                                // Explicit ValidAt as LINQ to Entity can't cope with interfaces
                                && x.Validity.Start <= validAt
                                && x.Validity.Finish >= validAt)
                            .FirstOrDefault();
        }

        public static IList<TMapping> FindAllMappings<TMapping>(this IRepository repository, MappingRequest request)
            where TMapping : class, IEntityMapping
        {
            return repository.FindAllMappings<TMapping>(request.SystemName, request.Identifier, request.ValidAt);
        }

        public static IList<TMapping> FindAllMappings<TMapping>(this IRepository repository, string sourceSystem, string mapping, DateTime validAt)
            where TMapping : class, IEntityMapping
        {
            return repository.Queryable<TMapping>()
                            .Where(x => x.System.Name == sourceSystem
                                && x.MappingValue == mapping
                                // Explicit ValidAt as LINQ to Entity can't cope with interfaces
                                && x.Validity.Start <= validAt
                                && x.Validity.Finish >= validAt)
                            .ToList();
        }

        public static bool EntityExistsFromNexusId<T>(this IRepository repository, EntityId nexusId) where T : class
        {
            int id;
            if (int.TryParse(nexusId.Identifier.Identifier, out id))
            {
                return repository.FindOne<T>(id) != null;
            }

            return false;
        }

        public static T FindEntityByMapping<T, TM>(this IRepository repository, EntityId entityId)
            where T : class, IEntity
            where TM : class, IEntityMapping
        {
            if (entityId == null)
            {
                return null;
            }

            var nexusId = entityId.Identifier;
                 
            if (nexusId.IsMdmId)
            {
                return repository.FindOne<T>(int.Parse(nexusId.Identifier));
            }

            var entity = (from x in repository.Queryable<TM>()
                          where x.System.Name == nexusId.SystemName
                             && x.MappingValue == nexusId.Identifier
                          select x)
                   .FirstOrDefault();

            if (entity == null)
            {
                return null;
            }

            return (T)entity.Entity;
        }

        /// <summary>
        /// Locate a SourceSystem by name
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Mdm.SourceSystem SystemByName(this IRepository repository, string name)
        {
            return repository.Queryable<Mdm.SourceSystem>()
                           .Where(x => x.Name == name)
                           .FirstOrDefault();
        }

        /// <summary>
        /// Is the target valid at the specified timepoint
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="validAt"></param>
        /// <returns></returns>
        public static IQueryable<T> ValidAt<T>(this IQueryable<T> queryable, DateTime validAt)
            where T : IRanged
        {
            return queryable
                    .Where(x => x.Validity.Start <= validAt
                             && x.Validity.Finish >= validAt);
        }

        /// <summary>
        /// Does the target validity overlap the range.
        /// <para>
        /// There are six cases
        /// <list type="">
        /// <li>Range is entirely before the validity period</li>
        /// <li>Range starts before validity start and ends before validity.finish</li>
        /// <li>Range starts after validity start and ends before validity.finish</li>
        /// <li>Range starts after validity start and ends after validity.finish</li>
        /// <li>Range starts after validity finish</li>
        /// <li>Range starts before validity start and ends after validity.finish</li>
        /// </list>
        /// So what we need to check is whether the range falls into case 1 or 5
        /// As DateRange good temporal semantics i.e. Start &lt; Finish we can use a simple inequality
        /// check; is the range finish before the validity start or the range.start after the validity.finish
        /// and then negate this.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static IQueryable<T> Overlaps<T>(this IQueryable<T> queryable, EnergyTrading.DateRange range)
            where T : IRanged
        {
            return queryable
                .Where(x => !(x.Validity.Start >= range.Finish 
                           || x.Validity.Finish <= range.Start));
        }

        /// <summary>
        /// Get a <see cref="ReferenceData"/> value by type.
        /// </summary>
        /// <param name="repository">Repository to use</param>
        /// <param name="referenceDataType">ReferenceDataType</param>
        /// <param name="value">Value to check</param>
        /// <returns></returns>
        public static bool ReferenceDataExists(this IRepository repository, string referenceDataType, string value)
        {
            var rd = repository.ReferenceData(referenceDataType, value);
            return rd != null;
        }

        /// <summary>
        /// Get a <see cref="ReferenceData"/> value by type.
        /// </summary>
        /// <param name="repository">Repository to use</param>
        /// <param name="referenceDataType">ReferenceDataType</param>
        /// <param name="value">Value to check</param>
        /// <returns></returns>
        public static Mdm.ReferenceData ReferenceData(this IRepository repository, string referenceDataType, string value)
        {
            return repository.Queryable<Mdm.ReferenceData>()
                             .FirstOrDefault(x => x.Key == referenceDataType
                                               && x.Value == value);
        }
    }
}
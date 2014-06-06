namespace EnergyTrading.Mdm.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.CSharp.RuntimeBinder;

    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>(
            this ObjectQuery<TEntity> query, Expression<Func<TEntity, TValue>> selector, IEnumerable<TValue> collection)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            ParameterExpression p = selector.Parameters.Single();

            if (!collection.Any())
            {
                return query;
            }

            IEnumerable<Expression> equals =
                collection.Select(
                    value => (Expression)Expression.Equal(selector.Body, Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate((accumulate, equal) => Expression.Or(accumulate, equal));

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> query, string path) where T : class
        {
            return QueryInclude(query, path);
        }

        public static IQueryable<TEntity> Include<TEntity, TValue>(this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector) where TEntity : class
        {
            return QueryIncludes(query, selector);
        }

        //Optional - to allow static collection:
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>(
            this ObjectQuery<TEntity> query, Expression<Func<TEntity, TValue>> selector, params TValue[] collection)
        {
            return WhereIn(query, selector, (IEnumerable<TValue>)collection);
        }

        private static T QueryInclude<T>(T query, string path)
        {
            dynamic querytWithIncludeMethod = query as dynamic;

            try
            {
                return (T)querytWithIncludeMethod.Include(path);
            }
            catch (RuntimeBinderException)
            {
                return query;
            }
        }

        private static T QueryIncludes<T, TV>(T query, TV selector)
        {
            dynamic querytWithIncludeMethod = query as dynamic;

            try
            {
                return (T)querytWithIncludeMethod.Include(selector);
            }
            catch (RuntimeBinderException)
            {
                return query;
            }
        }
    }
}
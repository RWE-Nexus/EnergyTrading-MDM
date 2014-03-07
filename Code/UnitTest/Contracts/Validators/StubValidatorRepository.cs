namespace EnergyTrading.MDM.Test.Contracts.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnergyTrading.Data;

    /// <summary>
    /// Used to return succssive non null stub values to the validators in tests. Required because of MOQ misses this feature
    /// </summary>
    public class StubValidatorRepository : IRepository
    {
        public T FindOne<T>(object id) where T : class, new()
        {
            return new T();
        }

        public IQueryable<T> Queryable<T>() where T : class
        {
            switch (typeof(T).Name)
            {
                case "SourceSystem":
                    return new List<SourceSystem>() as IQueryable<T>;
            }

            return null;
        }

        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Evict<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        // ouch. Moq can't cope with successive calls to the same method so this is the solution to allow for testing
        T IRepository.FindOne<T>(object id)
        {
            switch (typeof(T).Name)
            {

                case "SourceSystem":
                    return new SourceSystem() as T;
            }

            return default(T);
        }
    }
}
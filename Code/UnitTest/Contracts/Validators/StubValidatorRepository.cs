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
                case "Calendar":
                    return new List<Calendar>() as IQueryable<T>;

                case "Commodity":
                    return new List<Commodity>() as IQueryable<T>;

                case "CommodityInstrumentType":
                    return new List<CommodityInstrumentType>() as IQueryable<T>;

                case "Location":
                    return new List<Location>() as IQueryable<T>;

                case "Market":
                    return new List<Market>() as IQueryable<T>;

                case "Party":
                    return new List<Party>() as IQueryable<T>;

                case "Product":
                    return new List<Product>() as IQueryable<T>;

                case "ProductType":
                    return new List<ProductType>() as IQueryable<T>;

                case "ProductTypeInstance":
                    return new List<ProductTypeInstance>() as IQueryable<T>;

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
                case "Calendar":
                    return new Calendar() as T;

                case "Commodity":
                    return new Commodity() as T;

                case "CommodityInstrumentType":
                    return new CommodityInstrumentType() as T;

                case "Location":
                    return new Location() as T;

                case "Market":
                    return new Market() as T;

                case "Party":
                    return new Party() as T;

                case "Product":
                    return new Product() as T;

                case "ProductType":
                    return new ProductType() as T;

                case "ProductTypeInstance":
                    return new ProductTypeInstance() as T;

                case "SourceSystem":
                    return new SourceSystem() as T;

                case "TenorType":
                    return new TenorType() as T;
            }

            return default(T);
        }
    }
}
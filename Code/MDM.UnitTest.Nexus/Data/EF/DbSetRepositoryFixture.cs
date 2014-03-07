namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading.Data;
    using EnergyTrading.Test;

    using CheckerFactory = EnergyTrading.MDM.Test.CheckerFactory;

    /// <summary>
    /// Tests an IRepository based on DbSet
    /// </summary>
    /// <remarks>
    /// Copied from Nexus due to MSTest limitation on base classes in different assemblies.
    /// Also have to flatten inheritance hierarchy as MSTest only goes down one layer!
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    [TestClass, Ignore]
    public abstract class DbSetRepositoryFixture<T> : Fixture
        where T : class, IIdentifiable, new()
    {
        private DbContextProvider provider;
        private DbContextProvider provider2;
        private IRepository repository;
        private IRepository repository2;

        [ClassInitialize]
        public void ClassInitialize()
        {
            this.Zap();
        }

        [ClassCleanup]
        public void ClassCleanup()
        {
            this.Zap();
        }

        protected IDbContextProvider ContextProvider
        {
            get { return this.provider ?? (this.provider = new DbContextProvider(this.CreateDbContext)); }
        }

        protected IDbContextProvider ContextProvider2
        {
            get { return this.provider2 ?? (this.provider2 = new DbContextProvider(this.CreateDbContext)); }
        }

        protected DbContext Context
        {
            get { return this.ContextProvider.CurrentContext(); }
        }

        protected DbContext Context2
        {
            get { return this.ContextProvider2.CurrentContext(); }
        }

        protected IRepository Repository
        {
            get { return this.repository ?? (this.repository = new DbSetRepository(ContextProvider)); }
        }

        protected IRepository Repository2
        {
            get { return this.repository2 ?? (this.repository2 = new DbSetRepository(ContextProvider2)); }
        }

        /// <summary>
        /// Return the expected count after save operation.
        /// Override this Count, if actual entity has parent entity attribute and test data is setting up parent entity via ObjectMother
        /// </summary>
        /// <returns>Saved entities count</returns>
        protected virtual int ExpectedSavedEntitiesCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Return the expected count after Delete operation.
        /// Override this Count, if actual entity has parent entity attribute and test data is setting up parent entity via ObjectMother
        /// Delete entity will not delete any parents setup on save operation
        /// </summary>
        /// <returns>Deleted entities</returns>
        protected virtual int ExpectedEntitiesCountAfterDelete
        {
            get
            {
                return 0;
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            var expected = this.Default();

            using (var scope = new TransactionScope())
            {
                this.Repository.Add(expected);
                this.Repository.Flush();

                scope.Complete();
            }

            // NB Evict wipes collections - so won't compare correctly
            //this.Repository.Evict(expected);

            var count = (from x in Repository.Queryable<T>() select x).Count();
            Assert.AreEqual(ExpectedSavedEntitiesCount, count);

            // Get it back from the other context - forces a load
            var candidate = this.Repository2.FindOne<T>(expected.Id);
            Check(expected, candidate);
        }
		
		[TestMethod]
        [TestCategory("Integration")]
        public void Delete()
        {
            var expected = this.Default();

		    using (var scope = new TransactionScope())
		    {
		        this.Repository.Add(expected);
		        this.Repository.Flush();

		        scope.Complete();
		    }

		    var isEntityAdded = (from x in Repository.Queryable<T>() select x).Count() > 0;
		    Assert.IsTrue(isEntityAdded);

		    using (var scope = new TransactionScope())
		    {
		        this.Repository.Delete(expected);
		        this.Repository.Flush();

		        scope.Complete();
		    }

		    var count = (from x in Repository.Queryable<T>() select x).Count();
		    Assert.AreEqual(ExpectedEntitiesCountAfterDelete, count);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void RepositoryQuery()
        {
            var entities = from x in this.Repository.Queryable<T>() select x;

            var count = entities.Count();
        }

        protected override void TidyUp()
        {
            base.TidyUp();
            this.Zap();
        }

        protected override ICheckerFactory CreateCheckerFactory()
        {
            return new CheckerFactory();
        }

        protected DbContext CreateDbContext()
        {
            return new MappingContext();
        }

        protected virtual T Default()
        {
            var entity = new T();

            return entity;
        }

        protected virtual List<Action<IDbSetRepository>> Actions()
        {
            return new List<Action<IDbSetRepository>>();
        }

        protected void Zap()
        {
            var dao = (IDao)Repository;
            var zapper = new Zapper(dao);
            zapper.Zap();
        }
    }
}
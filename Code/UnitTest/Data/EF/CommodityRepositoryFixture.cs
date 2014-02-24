namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CommodityRepositoryFixture : DbSetRepositoryFixture<Commodity>
    {
        protected override Commodity Default()
        {
            var entity = ObjectMother.Create<Commodity>();

            return entity;
        }

        // Saves two entites, one for parent and one actual with reference parent
        protected override int ExpectedSavedEntitiesCount
        {
            get
            {
                return 2;
            }
        }

        // it will not delete the parent entity
        protected override int ExpectedEntitiesCountAfterDelete
        {
            get
            {
                return 1;
            }
        }
    }
}

namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ShapeElementRepositoryFixture : DbSetRepositoryFixture<ShapeElement>
    {
        protected override ShapeElement Default()
        {
            var entity = ObjectMother.Create<ShapeElement>();

            return entity;
        }
    }
}

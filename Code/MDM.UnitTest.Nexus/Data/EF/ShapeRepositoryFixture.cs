namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class ShapeRepositoryFixture : DbSetRepositoryFixture<Shape>
    {
        protected override Shape Default()
        {
            var entity = ObjectMother.Create<Shape>();

            return entity;
        }
    }
}

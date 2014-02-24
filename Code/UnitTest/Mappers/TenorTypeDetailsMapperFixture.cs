namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class TenorTypeDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var source = ObjectMother.Create<TenorType>();
            var mapper = new MDM.Mappers.TenorTypeDetailsMapper();

            var result = mapper.Map(source);

            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.ShortName, result.ShortName);
            //Assert.AreEqual(source.Traded.Start, result.Traded.StartDate);
            //Assert.AreEqual(source.Traded.Finish, result.Traded.EndDate);
        }
    }
}

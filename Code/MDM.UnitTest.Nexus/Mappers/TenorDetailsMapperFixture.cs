namespace EnergyTrading.MDM.Test.Mappers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EnergyTrading.MDM.Test;

    [TestClass]
    public class TenorDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var source = ObjectMother.Create<Tenor>();
            var mapper = new MDM.Mappers.TenorDetailsMapper();

            var result = mapper.Map(source);

            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.ShortName, result.ShortName);
            Assert.AreEqual(source.TenorType.Name, result.TenorType.Name);
            Assert.AreEqual(source.IsRelative, result.IsRelative);
            Assert.AreEqual(source.DeliveryPeriod, result.DeliveryPeriod);
            Assert.AreEqual(source.DeliveryRangeType, result.DeliveryRangeType);
            Assert.AreEqual(source.Delivery.Start, result.Delivery.StartDate);
            Assert.AreEqual(source.Delivery.Finish, result.Delivery.EndDate);
            Assert.AreEqual(source.Traded.Start, result.Traded.StartDate);
            Assert.AreEqual(source.Traded.Finish, result.Traded.EndDate);
        }
    }
}

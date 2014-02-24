namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;
    using TenorType = EnergyTrading.MDM.TenorType;

    [TestClass]
    public class TenorDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            var tenorType = new TenorType
            {
                Id = 1,
                Name = "tenorType",
            };
            var now = new DateTime(2013, 5, 14);
            var source = new TenorDetails
            {
                Name = "Name",
                ShortName = "ShortName",
                TenorType = new EntityId
                {
                    Identifier = new NexusId
                    {
                        Identifier = tenorType.Id.ToString(CultureInfo.InvariantCulture),
                        IsNexusId = true
                    },
                },
                IsRelative = false,
                DeliveryRangeType = "DeliveryRangeType",
                DeliveryPeriod = "DeliveryPeriod",
                Delivery = new DateRange { StartDate = now.AddDays(-1), EndDate = now },
                Traded = new DateRange { StartDate = now.AddDays(-2), EndDate = now },
            };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.FindOne<TenorType>(tenorType.Id)).Returns(tenorType);
            var mapper = new EnergyTrading.MDM.Contracts.Mappers.TenorDetailsMapper(mockRepository.Object);

            var result = mapper.Map(source);

            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.ShortName, result.ShortName);
            Assert.IsNotNull(result.TenorType);
            Assert.AreEqual(tenorType.Name, result.TenorType.Name);
            Assert.AreEqual(source.IsRelative, result.IsRelative);
            Assert.AreEqual(source.DeliveryRangeType, result.DeliveryRangeType);
            Assert.AreEqual(source.DeliveryPeriod, result.DeliveryPeriod);
            Assert.AreEqual(source.Delivery.EndDate.Value, result.Delivery.Finish);
            Assert.AreEqual(source.Traded.StartDate.Value, result.Traded.Start);
            Assert.AreEqual(source.Traded.EndDate.Value, result.Traded.Finish);
        }
    }
}

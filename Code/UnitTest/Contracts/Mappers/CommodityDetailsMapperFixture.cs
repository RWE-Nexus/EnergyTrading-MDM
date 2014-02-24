namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using EnergyTrading.Data;
    using RWEST.Nexus.MDM.Contracts;

    [TestClass]
    public class CommodityDetailsMapperFixture : Fixture
    {
        [TestMethod]
        public void Map()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var fakeParentCommodity = new MDM.Commodity() { Name = "fakeParentCommodity" };
            var fakeMassEnergyUnit = new MDM.Unit() { Name = "fakeMassEnergyUnit" };
            var fakeVolumeEnergyUnit = new MDM.Unit() { Name = "fakeVolumeEnergyUnit" };
            var fakeVolumetricDensityUnit = new MDM.Unit() { Name = "fakeVolumetricDensityUnit" };

            var source = new RWEST.Nexus.MDM.Contracts.CommodityDetails
                {
                    Name = "Test",
                    Parent =
                        new EntityId()
                            {
                                Identifier = new NexusId() { Identifier = "1", IsNexusId = true },
                                Name = "fakeParentCommodity"
                            },
                    MassEnergyUnits = this.CreateUnit(1, "fakeMassEnergyUnit"),
                    VolumeEnergyUnits = this.CreateUnit(2, "fakeVolumeEnergyUnit"),
                    VolumetricDensityUnits = this.CreateUnit(3, "fakeVolumetricDensityUnit"),
                    MassEnergyValue = 1300,
                    VolumeEnergyValue = 34.5m,
                    VolumetricDensityValue = 123.4m,
                    DeliveryRate = "Month"
                };

            var mapper = new EnergyTrading.MDM.Contracts.Mappers.CommodityDetailsMapper(mockRepository.Object);
            mockRepository.Setup(x => x.FindOne<MDM.Commodity>(1)).Returns(fakeParentCommodity);
            mockRepository.Setup(x => x.FindOne<MDM.Unit>(1)).Returns(fakeMassEnergyUnit);
            mockRepository.Setup(x => x.FindOne<MDM.Unit>(2)).Returns(fakeVolumeEnergyUnit);
            mockRepository.Setup(x => x.FindOne<MDM.Unit>(3)).Returns(fakeVolumetricDensityUnit);

            // Act
            var result = mapper.Map(source);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(source.Name, result.Name);
            Assert.IsNotNull(result.Parent);
            Assert.AreEqual(fakeParentCommodity.Name, result.Parent.Name);
            Assert.AreEqual(fakeMassEnergyUnit.Name, result.MassEnergyUnits.Name);
            Assert.AreEqual(fakeVolumeEnergyUnit.Name, result.VolumeEnergyUnits.Name);
            Assert.AreEqual(fakeVolumetricDensityUnit.Name, result.VolumetricDensityUnits.Name);
            Assert.AreEqual(source.MassEnergyValue, result.MassEnergyValue);
            Assert.AreEqual(source.VolumeEnergyValue, result.VolumeEnergyValue);
            Assert.AreEqual(source.VolumetricDensityValue, result.VolumetricDensityValue);
            Assert.AreEqual(source.DeliveryRate, result.DeliveryRate);
        }

        private EntityId CreateUnit(int identifier , string name)
        {
            return new EntityId()
                {
                    Identifier = new NexusId() { Identifier = identifier.ToString(), IsNexusId = true },
                    Name = name
                };
        }
    }
}
		
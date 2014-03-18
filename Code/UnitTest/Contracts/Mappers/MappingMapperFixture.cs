namespace EnergyTrading.MDM.Test.Contracts.Mappers
{
    using System;
    using System.Linq;

    using Moq;

    using EnergyTrading.MDM.Contracts.Mappers;

    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.Mdm;

    public class MappingMapperFixture : Fixture
    {
        public void Map<TMapping>()
           where TMapping : EntityMapping, new()
        {
            // Arrange
            var system = new SourceSystem { Name = "Test" };
            var systemList = new System.Collections.Generic.List<SourceSystem> { system };
            var repository = new Mock<IRepository>();
            repository.Setup(x => x.Queryable<SourceSystem>()).Returns(systemList.AsQueryable());

            var start = new DateTime(2010, 1, 1);
            var end = new DateTime(2012, 12, 31);
            var source = new EnergyTrading.Mdm.Contracts.MdmId { MappingId = 1, SystemName = "Test", Identifier = "1", StartDate = start, EndDate = end };

            var expected = new TMapping
            {               
                System = system,
                MappingValue = "1",
                Validity = new DateRange(start, end)
            };
            ((IEntityMapping)expected).MappingId = 1;

            var mapper = new MappingMapper<TMapping>(repository.Object);

            // Act
            var candidate = mapper.Map(source);

            // Assert
            Check(expected, candidate);
        }
    }
}
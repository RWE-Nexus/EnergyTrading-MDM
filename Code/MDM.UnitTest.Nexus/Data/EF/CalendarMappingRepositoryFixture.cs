namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class CalendarMappingRepositoryFixture : DbSetRepositoryFixture<CalendarMapping>
    {
        protected override CalendarMapping Default()
        {
            var entity = base.Default();
            entity.Calendar = ObjectMother.Create<Calendar>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class InstrumentTypeOverrideRepositoryFixture : DbSetRepositoryFixture<InstrumentTypeOverride>
    {
        protected override InstrumentTypeOverride Default()
        {
            var entity = ObjectMother.Create<InstrumentTypeOverride>();

            return entity;
        }
    }
}

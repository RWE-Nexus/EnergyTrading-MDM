namespace EnergyTrading.MDM.Test.Data.EF
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class InstrumentTypeRepositoryFixture : DbSetRepositoryFixture<InstrumentType>
    {
        protected override InstrumentType Default()
        {
            var entity = ObjectMother.Create<InstrumentType>();

            return entity;
        }
    }
}

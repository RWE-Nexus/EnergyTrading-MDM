namespace EnergyTrading.MDM.Test.Data.EF
{
    using NUnit.Framework;

    using EnergyTrading.Mdm;

    [TestFixture]
    public class SourceSystemRepositoryFixture : DbSetRepositoryFixture<SourceSystem>
    {
        protected override SourceSystem Default()
        {
            var entity = ObjectMother.Create<SourceSystem>();

            return entity;
        }
    }
}

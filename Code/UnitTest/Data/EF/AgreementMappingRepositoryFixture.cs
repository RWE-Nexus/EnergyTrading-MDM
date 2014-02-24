namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class AgreementMappingRepositoryFixture : DbSetRepositoryFixture<AgreementMapping>
    {
        protected override AgreementMapping Default()
        {
            var entity = base.Default();
            entity.Agreement = ObjectMother.Create<Agreement>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}

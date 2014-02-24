namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM;

    [TestClass]
    public class PartyRoleMappingRepositoryFixture : DbSetRepositoryFixture<PartyRoleMapping>
    {
        protected override PartyRoleMapping Default()
        {
            var entity = base.Default();
            entity.PartyRole = ObjectMother.Create<PartyRole>();
            entity.System = new SourceSystem { Name = Guid.NewGuid().ToString() };
            entity.MappingValue = "Test";

            return entity;
        }
    }
}
namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;

    using NUnit.Framework;

    using EnergyTrading.Mdm;

    [TestFixture]
    public class ReferenceDataRepositoryFixture : DbSetRepositoryFixture<ReferenceData>
    {        
        protected override ReferenceData Default()
        {
            return new ReferenceData { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString() };
        }
    }
}
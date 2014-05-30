namespace EnergyTrading.MDM.Test.Data.EF
{
    using System;

    using EnergyTrading.Mdm;

    using NUnit.Framework;

    [TestFixture]
    public class ReferenceDataRepositoryFixture : DbSetRepositoryFixture<ReferenceData>
    {        
        protected override ReferenceData Default()
        {
            return new ReferenceData { Key = Guid.NewGuid().ToString(), Value = Guid.NewGuid().ToString() };
        }
    }
}
namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public class FeeTypeDataChecker
    {
        public void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.FeeType contract, MDM.FeeType entity)
        {
            FeeTypeComparer.Compare(contract, entity);
        }

        public void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.FeeType contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.FeeType>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }

        public void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.FeeType contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.FeeType>(new MappingContext()).FindOne(id);

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

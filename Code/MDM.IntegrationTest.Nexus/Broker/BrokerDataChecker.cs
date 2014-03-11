namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class BrokerDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.Broker contract, MDM.Broker entity)
        {
            MDM.BrokerDetails correctDetail = null;

            if (entity.Details.Count == 1)
            {
                correctDetail = entity.Details[0] as MDM.BrokerDetails;
            }
            else
            {
                correctDetail = (MDM.BrokerDetails)entity.Details.Where(
                    x => x.Validity.Start >= contract.Nexus.StartDate && x.Validity.Finish >= contract.Nexus.EndDate).First();
            }

            Assert.AreEqual(contract.Details.Name, correctDetail.Name);
            Assert.AreEqual(contract.Details.Phone, correctDetail.Phone);
            Assert.AreEqual(contract.Details.Fax, correctDetail.Fax);
            Assert.AreEqual(contract.Details.Rate, correctDetail.Rate);
            Assert.AreEqual(contract.Party.NexusId(), entity.Party.Id);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.Broker contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.Broker>(new NexusMappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.Broker contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.Broker>(new NexusMappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using EnergyTrading.Data.EntityFramework;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;
    using EnergyTrading;

    public static class BusinessUnitDataChecker
    {
        public static void CompareContractWithEntityDetails(RWEST.Nexus.MDM.Contracts.BusinessUnit contract, MDM.BusinessUnit entity)
        {
            MDM.BusinessUnitDetails correctDetail = null;

            if (entity.Details.Count == 1)
            {
                correctDetail = entity.Details[0] as MDM.BusinessUnitDetails;
            }
            else
            {
                correctDetail = (MDM.BusinessUnitDetails)entity.Details.Where(
                    x => x.Validity.Start >= contract.Nexus.StartDate && x.Validity.Finish >= contract.Nexus.EndDate).First();
            }

            Assert.AreEqual(contract.Details.Name, correctDetail.Name);
            Assert.AreEqual(contract.Details.Phone, correctDetail.Phone);
            Assert.AreEqual(contract.Details.Fax, correctDetail.Fax);
            Assert.AreEqual(contract.Details.AccountType, correctDetail.AccountType);
            Assert.AreEqual(contract.Details.Address, correctDetail.Address);
            Assert.AreEqual(contract.Details.Status, correctDetail.Status);
            Assert.AreEqual(contract.Details.TaxLocation.NexusId().Value, correctDetail.TaxLocation.Id);
            Assert.AreEqual(contract.Party.NexusId().Value, entity.Party.Id);
        }

        public static void ConfirmEntitySaved(int id, RWEST.Nexus.MDM.Contracts.BusinessUnit contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.BusinessUnit>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(RWEST.Nexus.MDM.Contracts.BusinessUnit contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.BusinessUnit>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

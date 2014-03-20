namespace EnergyTrading.MDM.Test
{
    using System.Linq;

    using EnergyTrading.Data.EntityFramework;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Data.EF.Configuration;

    public class SourceSystemDataChecker
    {
        public void CompareContractWithEntityDetails(SourceSystem contract, MDM.SourceSystem entity)
        {
            SourceSystemComparer.Compare(contract, entity);
        }

        public void ConfirmEntitySaved(int id, SourceSystem contract)
        {
            var savedEntity =
                new DbSetRepository(new DbContextProvider(() => new MappingContext())).FindOne<MDM.SourceSystem>(id);
            contract.Identifiers.Add(new MdmId() { IsMdmId = true, Identifier = id.ToString() });

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }

        public void CompareContractWithSavedEntity(EnergyTrading.Mdm.Contracts.SourceSystem contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsMdmId).First().Identifier);
            var savedEntity = new DbSetRepository(new DbContextProvider(() => new MappingContext())).FindOne<MDM.SourceSystem>(id);

            this.CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

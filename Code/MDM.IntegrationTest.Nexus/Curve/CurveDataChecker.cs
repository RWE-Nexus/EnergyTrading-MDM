namespace RWEST.Nexus.MDM.Test
{
    using System.Linq;
    using Nexus.Data.EntityFramework;
    using Contracts;
    using Data.EF.Configuration;

    public static class CurveDataChecker
    {
        public static void CompareContractWithEntityDetails(Contracts.Curve contract, MDM.Curve entity)
        {
			CurveComparer.Compare(contract, entity);
        }

        public static void ConfirmEntitySaved(int id, Contracts.Curve contract)
        {
            var savedEntity =
                new DbSetRepository<MDM.Curve>(new MappingContext()).FindOne(id);
            contract.Identifiers.Add(new NexusId() { IsNexusId = true, Identifier = id.ToString() });

            CompareContractWithEntityDetails(contract, savedEntity);
        }

        public static void CompareContractWithSavedEntity(Contracts.Curve contract)
        {
            int id = int.Parse(contract.Identifiers.Where(x => x.IsNexusId).First().Identifier);
            var savedEntity = new DbSetRepository<MDM.Curve>(new MappingContext()).FindOne(id);

            CompareContractWithEntityDetails(contract, savedEntity);
        }
    }
}

namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;

    public class NexusIdChecker : Checker<EnergyTrading.Mdm.Contracts.MdmId>
    {
        public NexusIdChecker()
        {
            Compare(x => x.Header);
            Compare(x => x.SystemId);
            Compare(x => x.SystemName);
            Compare(x => x.Identifier);
            Compare(x => x.MappingId);
            Compare(x => x.SourceSystemOriginated);
            Compare(x => x.DefaultReverseInd);
            Compare(x => x.StartDate);
            Compare(x => x.EndDate);
        }
    }
}

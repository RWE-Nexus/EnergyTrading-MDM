namespace EnergyTrading.MDM.Messages.Services
{
    public class GetMappingRequestFaultHandler : FaultHandler<GetMappingRequest>
    {
        protected override string Reason(ContractError error)
        {
            return "Unknown Mapping";
        }

        protected override string NotFoundErrorMessage(string entityName, ContractError error, GetMappingRequest request)
        {
            return string.Format("Mapping identified by '{2}' not found", entityName, request.EntityId, request.MappingId);
        }

        protected override void PopulateFault(RWEST.Nexus.MDM.Contracts.Fault fault, GetMappingRequest request)
        {
        }
    }
}
namespace EnergyTrading.Mdm.Messages.Services
{
    public class CrossMappingRequestFaultHandler : FaultHandler<CrossMappingRequest>
    {
        protected override string Reason(ContractError error)
        {
            return "Unknown Mapping";
        }

        protected override string NotFoundErrorMessage(string entityName, ContractError error, CrossMappingRequest request)
        {
            return string.Format("Mapping String '{1}' not found for Source System '{0}'", request.SystemName, request.Identifier, request.TargetSystemName);
        }

        protected override void PopulateFault(EnergyTrading.Mdm.Contracts.Fault fault, CrossMappingRequest request)
        {
            fault.SourceSystem = request.SystemName;
            fault.Mapping = request.Identifier;
        }        
    }
}
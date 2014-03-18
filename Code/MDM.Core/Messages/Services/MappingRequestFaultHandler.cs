namespace EnergyTrading.MDM.Messages.Services
{
    public class MappingRequestFaultHandler : FaultHandler<MappingRequest>
    {
        protected override string Reason(ContractError error)
        {
            return "Unknown Mapping";
        }

        protected override string NotFoundErrorMessage(string entityName, ContractError error, MappingRequest request)
        {
            switch (error.Reason)
            {
                case ErrorReason.SourceSystem:
                    return string.Format("Source System '{0}' does not exist", request.SystemName);

                default:
                    return string.Format("Mapping String '{0}' not found for Source System '{1}'", request.Identifier, request.SystemName);
            }
        }
        
        protected override void PopulateFault(EnergyTrading.Mdm.Contracts.Fault fault, MappingRequest request)
        {
            fault.SourceSystem = request.SystemName;
            fault.Identifier = request.Identifier;
        }
    }
}
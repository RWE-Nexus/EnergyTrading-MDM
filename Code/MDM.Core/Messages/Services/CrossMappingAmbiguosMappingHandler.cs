namespace EnergyTrading.Mdm.Messages.Services
{
    public class CrossMappingAmbiguosMappingHandler : FaultHandler<CrossMappingRequest>
    {
        protected override string Reason(ContractError error)
        {
            return "Ambiguous Mapping Found";
        }

        protected override string NotFoundErrorMessage(string entityName, ContractError error, CrossMappingRequest request)
        {
            return string.Format("Ambiguous Mappings were found for Mapping String '{1}' for Source System '{0}' and Destination System '{2}'", request.SystemName, request.Identifier, request.TargetSystemName);
        }

        protected override void PopulateFault(EnergyTrading.Mdm.Contracts.Fault fault, CrossMappingRequest request)
        {
            fault.SourceSystem = request.SystemName;
            fault.Mapping = request.Identifier;
        }        
    }
}
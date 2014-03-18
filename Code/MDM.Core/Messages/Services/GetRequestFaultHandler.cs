namespace EnergyTrading.MDM.Messages.Services
{
    public class GetRequestFaultHandler : FaultHandler<GetRequest>
    {
        protected override string NotFoundErrorMessage(string entityName, ContractError error, GetRequest request)
        {
            return string.Format("{0} identified by '{1}' not found", entityName, request.EntityId);
        }

        protected override void PopulateFault(EnergyTrading.Mdm.Contracts.Fault fault, GetRequest request)
        {
            fault.Identifier = request.EntityId.ToString();
        }
    }
}
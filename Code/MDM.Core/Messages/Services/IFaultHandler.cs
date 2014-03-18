namespace EnergyTrading.MDM.Messages.Services
{
    using EnergyTrading.Mdm.Contracts;

    public interface IFaultHandler<in T>
        where T : ReadRequest
    {
        Fault Create(string entityName, ContractError error, T request);
    }
}
namespace EnergyTrading.MDM.Messages
{
    using System;

    /// <summary>
    /// Wrappers a contract along with some other metadata
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public class ContractResponse<TContract>
    {
        public ContractResponse()
        {
            Error = new ContractError();
            IsValid = true;
        }

        public TContract Contract { get; set; }

        public ulong Version { get; set; }

        public ContractError Error { get; set; }

        public bool IsValid { get; set; }
    }
}
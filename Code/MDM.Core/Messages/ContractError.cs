namespace EnergyTrading.MDM.Messages
{
    public class ContractError
    {
        public ErrorType Type { get; set; }

        public ErrorReason Reason { get; set; }
    }
}
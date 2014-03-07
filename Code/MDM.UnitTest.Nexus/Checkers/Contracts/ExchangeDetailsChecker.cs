namespace EnergyTrading.MDM.Test.Checkers.Contract
{
    using EnergyTrading.Test;

    using ExchangeDetails = RWEST.Nexus.MDM.Contracts.ExchangeDetails;

    public class ExchangeDetailsChecker : Checker<ExchangeDetails>
    {
        public ExchangeDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.Phone);
            Compare(x => x.Fax);
        }
    }
}

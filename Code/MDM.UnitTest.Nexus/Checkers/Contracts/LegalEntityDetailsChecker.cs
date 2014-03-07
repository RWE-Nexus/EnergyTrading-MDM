namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using EnergyTrading.Test;
    using LegalEntityDetails = RWEST.Nexus.MDM.Contracts.LegalEntityDetails;

    public class LegalEntityDetailsChecker : Checker<LegalEntityDetails>
    {
        public LegalEntityDetailsChecker()
        {
            Compare(x => x.Name);
            Compare(x => x.RegisteredName);
            Compare(x => x.RegistrationNumber);
            Compare(x => x.Address);
            Compare(x => x.Website);
            Compare(x => x.Email);
            Compare(x => x.Fax);
            Compare(x => x.Phone);
            Compare(x => x.CountryOfIncorporation);
            Compare(x => x.PartyStatus);
        }
    }
}

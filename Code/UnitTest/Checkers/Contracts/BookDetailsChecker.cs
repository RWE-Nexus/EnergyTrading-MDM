namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    using System;

    using RWEST.Nexus.MDM;
    using RWEST.Nexus.MDM.Contracts;
    using EnergyTrading.Test;

    public class BookDetailsChecker : Checker<BookDetails>
    {
        public BookDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}

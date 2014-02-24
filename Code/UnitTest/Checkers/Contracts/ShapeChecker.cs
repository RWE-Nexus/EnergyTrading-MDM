using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnergyTrading.Test;

namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class ShapeChecker : Checker<RWEST.Nexus.MDM.Contracts.Shape>
    {
        public ShapeChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
            Compare(x => x.Nexus); 
            Compare(x => x.Audit);
            Compare(x => x.Links);
        }
    }
}

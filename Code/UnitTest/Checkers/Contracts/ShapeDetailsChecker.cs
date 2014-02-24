using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnergyTrading.Test;

namespace EnergyTrading.MDM.Test.Checkers.Contracts
{
    public class ShapeDetailsChecker : Checker<RWEST.Nexus.MDM.Contracts.ShapeDetails>
    {
        public ShapeDetailsChecker()
        {
            Compare(x => x.Name);
        }
    }
}

using System;
using EnergyTrading.Mdm.Contracts;

namespace MDM.ServiceHost.WebApi.Infrastructure.Exceptions
{
    public class MdmFaultException : Exception
    {
        public Fault Fault { get; private set; }

        public MdmFaultException(Fault fault)
        {
            Fault = fault;
        }
    }
}
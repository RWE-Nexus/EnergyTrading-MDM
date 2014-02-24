namespace EnergyTrading.MDM.Test
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using Contracts;
    using Microsoft.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RWEST.Nexus.MDM.Contracts;

    public static class HttpResponseExtensions
    {
        public static void AssertStatusCodeIs(this HttpResponseMessage response, HttpStatusCode httpStatusCode)
        {
            if (response.StatusCode != httpStatusCode)
            {
                var fault = FaultFor(response);
                Assert.Fail(fault.Message);
            }
        }

        private static Fault FaultFor(HttpResponseMessage response)
        {
            Fault fault;
            try
            {
                fault = response.Content.ReadAsDataContract<Fault>();
            }
            catch (Exception)
            {
                fault = new Fault() {Message = response.StatusCode.ToString()};
            }
            return fault;
        }
    }
}
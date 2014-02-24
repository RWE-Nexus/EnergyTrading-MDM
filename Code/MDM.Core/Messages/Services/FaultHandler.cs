namespace EnergyTrading.MDM.Messages.Services
{
    using RWEST.Nexus.MDM.Contracts;

    public abstract class FaultHandler<T> : IFaultHandler<T>
        where T : ReadRequest
    {
        public Fault Create(string entityName, ContractError error, T request)
        {
            var fault = new Fault
            {
                Reason = string.Format(Reason(error), entityName),
            };
            this.PopulateFault(fault, request);

            var message = this.ErrorMessage(entityName, error, request);
            if (request.ValidAtExists)
            {
                message += string.Format(" at the given date '{0}'", request.ValidAt);
                fault.AsOfDate = request.ValidAt;
            }

            fault.Message = message;

            return fault;
        }

        protected virtual string Reason(ContractError error)
        {
            switch (error.Type)
            {
                case ErrorType.NotFound:
                    return "Unknown {0}";

                case ErrorType.VersionConflict:
                    return "Version Conflict";

                default:
                    return "Unknown {0}";
            }
        }

        protected string ErrorMessage(string entityName, ContractError error, T request)
        {
            switch (error.Type)
            {
                case ErrorType.Ambiguous:
                    return this.NotFoundErrorMessage(entityName, error, request);

                case ErrorType.NotFound:
                    return this.NotFoundErrorMessage(entityName, error, request);

                case ErrorType.VersionConflict:
                    return "Version Conflict";

                default:
                    return "Unknown";
            }
        }

        protected abstract string NotFoundErrorMessage(string entityName, ContractError error, T request);
                
        protected virtual void PopulateFault(Fault fault, T request)
        {            
        }
    }
}
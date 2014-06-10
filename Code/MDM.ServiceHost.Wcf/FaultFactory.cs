namespace EnergyTrading.Mdm.ServiceHost.Wcf
{
    using System;
    using System.Net;
    using System.ServiceModel.Web;

    using EnergyTrading.Extensions;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Messages;
    using EnergyTrading.Mdm.Messages.Services;
    using EnergyTrading.Mdm.Services;
    using EnergyTrading.Validation;

    public static class FaultFactory
    {
        public static WebFaultException<Fault> Exception<T>(string entityName, ContractResponse<T> contract, GetRequest request)
        {
            switch (contract.Error.Type)
            {
                case ErrorType.NotFound:
                    return NotFoundException(entityName, contract, request);

                default:
                    return NotFoundException(entityName, contract, request);
            }
        }

        public static WebFaultException<Fault> Exception<T>(string entityName, ContractResponse<T> contract, GetMappingRequest request)
        {
            switch (contract.Error.Type)
            {
                case ErrorType.NotFound:
                    return NotFoundException(entityName, contract, request);

                default:
                    return NotFoundException(entityName, contract, request);
            }
        }

        public static WebFaultException<Fault> Exception<T>(string entityName, ContractResponse<T> contract, MappingRequest request)
        {
            switch (contract.Error.Type)
            {
                case ErrorType.NotFound:
                    return NotFoundException(entityName, contract, request);

                default:
                    return NotFoundException(entityName, contract, request);
            }
        }

        public static WebFaultException<Fault> Exception<T>(string entityName, ContractResponse<T> contract, CrossMappingRequest request)
        {
            switch (contract.Error.Type)
            {
                case ErrorType.Ambiguous:
                    return AmbiguousMapping(entityName, contract, request);

                case ErrorType.NotFound:
                    return NotFoundException(entityName, contract, request);

                default:
                    return NotFoundException(entityName, contract, request);
            }
        }

        public static WebFaultException<Fault> NotFoundException<T>(string entityName, ContractResponse<T> contract, GetRequest request)
        {
            var handler = new GetRequestFaultHandler();
            var fault = handler.Create(entityName, contract.Error, request);

            return new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }

        public static WebFaultException<Fault> NotFoundException<T>(string entityName, ContractError error, GetRequest request)
        {
            var handler = new GetRequestFaultHandler();
            var fault = handler.Create(entityName, error, request);

            return new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }

        public static WebFaultException<Fault> NotFoundException<T>(string entityName, ContractResponse<T> contract, GetMappingRequest request)
        {
            var handler = new GetMappingRequestFaultHandler();
            var fault = handler.Create(entityName, contract.Error, request);

            return new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }

        public static WebFaultException<Fault> NotFoundException<T>(string entityName, ContractResponse<T> contract, MappingRequest request)
        {
            var handler = new MappingRequestFaultHandler();
            var fault = handler.Create(entityName, contract.Error, request);

            return new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }

        public static WebFaultException<Fault> NotFoundException<T>(string entityName, ContractResponse<T> contract, CrossMappingRequest request)
        {
            var handler = new CrossMappingRequestFaultHandler();
            var fault = handler.Create(entityName, contract.Error, request);

            return new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }

        public static WebFaultException<Fault> AmbiguousMapping<T>(string entityName, ContractResponse<T> contract, CrossMappingRequest request)
        {
            var handler = new CrossMappingAmbiguosMappingHandler();
            var fault = handler.Create(entityName, contract.Error, request);

            return new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }

        public static WebFaultException<Fault> Exception(Exception ex)
        {
            var fault = new Fault
                {
                    Message = ex.AllExceptionMessages(),
                    Reason = "Unknown"
            };

            return new WebFaultException<Fault>(fault, HttpStatusCode.InternalServerError);           
        }

        public static WebFaultException<Fault> ValidationException(ValidationException ex)
        {
            var fault = new Fault
            {
                Message = ex.Message,
                Reason = "Validation failure"
            };

            throw new WebFaultException<Fault>(fault, HttpStatusCode.BadRequest);            
        }

        public static WebFaultException<Fault> VersionConflictException(VersionConflictException ex)
        {
            var fault = new Fault
            {
                Message = ex.Message,
                Reason = "Validation failure"
            };

            throw new WebFaultException<Fault>(fault, HttpStatusCode.PreconditionFailed);
        }

        public static WebFaultException<Fault> SearchResultNotFoundException(string searchKey, string pageNumber)
        {
            var searchResultsNotFound = string.Format("Search results not found for key {0}/{1}", searchKey, pageNumber);
            var fault = new Fault { Message = searchResultsNotFound, Reason = searchResultsNotFound };

            throw new WebFaultException<Fault>(fault, HttpStatusCode.NotFound);
        }
    }
}
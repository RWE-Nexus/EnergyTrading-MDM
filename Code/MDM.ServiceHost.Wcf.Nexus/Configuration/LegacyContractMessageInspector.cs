namespace EnergyTrading.MDM.ServiceHost.Wcf.Nexus.Configuration
{
    using System;
    using System.Collections.ObjectModel;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Configuration;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    using EnergyTrading.Container.Unity;

    using Search = EnergyTrading.Contracts.Search.Search;

    public class LegacyContractMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
//            var bob = request.ToString();
//            var bob2 = request.GetBody<Search>();
//            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
//            request = buffer.CreateMessage();
//            var originalMessage = buffer.CreateMessage();
//            Console.WriteLine("Received:\n{0}", originalMessage.ToString());
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
//            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
//            reply = buffer.CreateMessage();
//            Console.WriteLine("Sending:\n{0}", buffer.CreateMessage().ToString());
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class LegacyContractServiceBehavior : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            for (int i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
            {
                var channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
                if (channelDispatcher != null)
                {
                    foreach (var endpointDispatcher in channelDispatcher.Endpoints)
                    {
                        var inspector = new LegacyContractMessageInspector();
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

    public class LegacyContractBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            throw new Exception("Behavior not supported on the consumer side!");
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            var inspector = new LegacyContractMessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }

    public class LegacyContractBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new LegacyContractBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(LegacyContractBehavior);
            }
        }
    }
}
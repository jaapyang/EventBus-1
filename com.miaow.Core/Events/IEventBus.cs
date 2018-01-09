using System;
using System.Reflection;

namespace com.miaow.Core.Events
{
    public interface IEventBus
    {
        void Register(Type requestType);
        void Register<TRequest>() where TRequest : IEventRequest;
        void RegisterAllEventHandlerFromAssembly(Assembly assembly);

        bool HasRegisted<TRequest>() where TRequest : IEventRequest;
        bool HasRegisted(Type requestType);

        TResponse Trigger<TRequest, TResponse>(TRequest request)
            where TRequest : IEventRequest
            where TResponse : IEventResponse, new();
    }
}
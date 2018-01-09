using System.Reflection;

namespace com.miaow.Core.Events
{
    public interface IEventBus
    {
        void Register<TRequest>() where TRequest : IEventRequest;
        void RegisterAllEventHandlerFromAssembly(Assembly assembly);
        TResponse Trigger<TRequest, TResponse>(TRequest request);
    }
}
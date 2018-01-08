using System;
using System.Reflection;

namespace com.miaow.Core.EventBus
{
    public interface IEventBus
    {
        void Register<TEventArgs>(IEventHandler eventHandler) where TEventArgs : IEventArgs;
        void Register(Type eventType, Type handlerType);
        void RegisterAllEventHandlerFromAssembly(Assembly assembly);

        void UnRegister<TEventArgs>(Type handlerType) where TEventArgs : IEventArgs;

        void Trigger<TEventArgs>(TEventArgs eventArgs) where TEventArgs : IEventArgs;
    }
}
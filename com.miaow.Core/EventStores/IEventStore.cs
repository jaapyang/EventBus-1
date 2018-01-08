using System;
using System.Collections.Generic;
using com.miaow.Core.EventBus;

namespace com.miaow.Core.EventStores
{
    public interface IEventStore
    {
        void AddRegister(Type argsType, Type handlerType);

        void RemoveRegister(Type argsType, Type eventHandler);

        bool HasRegisterForEvent(Type eventData);

        bool IsEmpty { get; }

        void Clear();

        IEnumerable<Type> GetHandlersForEvent(Type eventArgsType);
        IEnumerable<Type> GetHandlersForEvent<TEventArgs>() where TEventArgs : IEventArgs;
    }
}
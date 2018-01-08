using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using com.miaow.Core.EventBus;
using System.Linq;

namespace com.miaow.Core.EventStores
{
    public class MemoryEventStore : IEventStore
    {
        private static readonly  object lockObj = new object();

        private readonly ConcurrentDictionary<Type, List<Type>> _argsAndHandlerMapping;

        public MemoryEventStore()
        {
            _argsAndHandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
        }

        public void AddRegister(Type argsType, Type handlerType)
        {
            lock (lockObj)
            {
                if (!HasRegisterForEvent(argsType))
                {
                    var handlers = new List<Type>();
                    _argsAndHandlerMapping.TryAdd(argsType, handlers);
                }

                if (_argsAndHandlerMapping[argsType].All(x => x != handlerType))
                {
                    _argsAndHandlerMapping[argsType].Add(handlerType);
                }
            }
        }

        public void RemoveRegister(Type argsType, Type eventHandler)
        {
            throw new NotImplementedException();
        }

        public bool HasRegisterForEvent(Type eventData)
        {
           
        }

        public bool IsEmpty { get; }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> GetHandlersForEvent(Type eventArgsType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> GetHandlersForEvent<TEventArgs>() where TEventArgs : IEventArgs
        {
            throw new NotImplementedException();
        }
    }
}
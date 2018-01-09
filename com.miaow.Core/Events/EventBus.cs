using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace com.miaow.Core.Events
{
    public class EventBus : IEventBus
    {
        public static EventBus Current { get; private set; }
        private readonly ConcurrentDictionary<Type, List<Type>> _requestAndhandlerMapping;

        static EventBus()
        {
            Current = new EventBus();
        }

        public EventBus()
        {
            _requestAndhandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
        }

        public void Register<TRequest>() where TRequest : IEventRequest
        {
            var requestType = typeof(TRequest);
            var handlerTypeName = $"{requestType.FullName}Handler";
            var handlerType = requestType.Assembly.GetType(handlerTypeName, false, false);

            if (!_requestAndhandlerMapping.ContainsKey(requestType))
            {
                _requestAndhandlerMapping.TryAdd(requestType, new List<Type>());
            }

            if (handlerType != null && typeof(IEventHandler).IsAssignableFrom(handlerType))
            {
                _requestAndhandlerMapping[requestType].Add(handlerType);
            }
        }

        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        public TResponse Trigger<TRequest, TResponse>(TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
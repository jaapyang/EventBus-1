using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;

namespace com.miaow.Core.Events
{
    public class EventBus : IEventBus
    {
        public static IEventBus Current { get; private set; }
        private readonly ConcurrentDictionary<Type, Type> _requestAndhandlerMapping;
        private readonly object _lockObj = new object();

        static EventBus()
        {
            Current = new EventBus();
        }

        public EventBus()
        {
            _requestAndhandlerMapping = new ConcurrentDictionary<Type, Type>();
        }

        public void Register<TRequest>() where TRequest : IEventRequest
        {
            var requestType = typeof(TRequest);
            Register(requestType);
        }

        public void Register(Type requestType)
        {
            var handlerTypeName = $"{requestType.FullName}Handler";
            var handlerType = requestType.Assembly.GetType(handlerTypeName, false, false);

            if (HasRegisted(requestType)) return;

            if (handlerType == null || !typeof(IEventHandler).IsAssignableFrom(handlerType)) return;

            lock (_lockObj)
            {
                _requestAndhandlerMapping.TryAdd(requestType, handlerType);
            }
        }

        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
            var requestInterfaceType = typeof(IEventRequest);
            var requestList = assembly.GetTypes().Where(x => requestInterfaceType.IsAssignableFrom(x)).ToList();
            foreach (var requeType in requestList)
            {
                Register(requeType);
            }
        }

        public bool HasRegisted<TRequest>() where TRequest : IEventRequest
        {
            return HasRegisted(typeof(TRequest));
        }

        public bool HasRegisted(Type requestType)
        {
            lock (_lockObj)
            {
                return _requestAndhandlerMapping.ContainsKey(requestType);
            }
        }

        private Type GetHandler(Type requestType)
        {
            lock (_lockObj)
            {
                return _requestAndhandlerMapping[requestType];
            }
        }

        public TResponse Trigger<TRequest, TResponse>(TRequest request)
            where TRequest : IEventRequest
            where TResponse : IEventResponse, new()
        {
            var handlerType = GetHandler(typeof(TRequest));

            var handler = handlerType.CreateInstance<IEventHandler<TRequest, TResponse>>();

            var result = handler.HandleEvent(request);

            return result;
        }
    }
}
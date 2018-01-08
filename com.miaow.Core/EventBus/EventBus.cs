using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.miaow.Core.EventStores;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace com.miaow.Core.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IEventStore _eventStore;
        public IWindsorContainer IocContainer { get; private set; }
        public static  EventBus Current { get; private set; }

        public EventBus()
        {
            
        }

        static EventBus()
        {
            Current = new EventBus();
        }

        public void Register<TEventArgs>(IEventHandler eventHandler) where TEventArgs : IEventArgs
        {
            Register(typeof(TEventArgs), eventHandler.GetType());
        }

        public void Register(Type eventType, Type handlerType)
        {
            var handlerInterface = handlerType.GetInterface("IEventHandler`1");
            if (!IocContainer.Kernel.HasComponent(handlerInterface))
            {
                IocContainer.Register(Component.For(handlerInterface, handlerType));
            }

            _eventStore.AddRegister(eventType, handlerType);
        }

        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
            //1.将IEventHandler注册到Ioc容器
            IocContainer.Register(Classes.FromAssembly(assembly)
                .BasedOn(typeof(IEventHandler<>))
                .WithService.Base());

            //2.从IOC容器中获取注册的所有IEventHandler
            var handlers = IocContainer.Kernel.GetAssignableHandlers(typeof(IEventHandler));
            foreach (var handler in handlers)
            {
                //循环遍历所有的IEventHandler<T>
                var interfaces = handler.ComponentModel.Implementation.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).IsAssignableFrom(@interface))
                    {
                        continue;
                    }

                    //获取泛型参数类型
                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Any())
                    {
                        var firstArg = genericArgs.FirstOrDefault();
                        if (firstArg != null)
                        {
                            //注册到事件源与事件处理的映射字典中
                            Register(firstArg, handler.ComponentModel.Implementation);
                        }
                    }
                }
            }
        }

        public void UnRegister<TEventArgs>(Type handlerType) where TEventArgs : IEventArgs
        {
            _eventStore.RemoveRegister(typeof(TEventArgs), handlerType);
        }

        public void Trigger<TEventArgs>(TEventArgs eventArgs) where TEventArgs : IEventArgs
        {
            List<Type> handlerTypes = _eventStore.GetHandlersForEvent(eventArgs.GetType()).ToList();

            if (handlerTypes.Count > 0)
            {
                foreach (var handlerType in handlerTypes)
                {
                    //从Ioc容器中获取所有的实例
                    var handlerInterface = handlerType.GetInterface("IEventHandler`1");
                    var eventHandlers = IocContainer.ResolveAll(handlerInterface);

                    //循环遍历，仅当解析的实例类型与映射字典中事件处理类型一致时，才触发事件
                    foreach (var eventHandler in eventHandlers)
                    {
                        if (eventHandler.GetType() == handlerType)
                        {
                            var handler = eventHandler as IEventHandler<TEventArgs>;
                            handler?.HandleEvent(eventArgs);
                        }
                    }
                }
            }
        }
    }
}
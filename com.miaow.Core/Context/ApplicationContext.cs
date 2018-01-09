using com.miaow.Core.Events;

namespace com.miaow.Core.Context
{
    public class ApplicationContext
    {
        public static ApplicationContext Current { get; }

        public IEventBus EventBus { get; }

        static ApplicationContext()
        {
           Current = new ApplicationContext();
        }

        private ApplicationContext()
        {
            EventBus = Events.EventBus.Current;
        }
    }
}
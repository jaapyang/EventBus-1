namespace com.miaow.Core.EventBus
{
    public abstract class EventHandlerBase<TEventArgs> : IEventHandler<TEventArgs> where TEventArgs : IEventArgs
    {
        public void HandleEvent(TEventArgs eventArgs)
        {
            HandleProcess(eventArgs);
        }

        protected abstract void HandleProcess(TEventArgs eventArgs);
    }
}
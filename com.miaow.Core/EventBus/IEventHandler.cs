namespace com.miaow.Core.EventBus
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TEventArgs> : IEventHandler
        where TEventArgs : IEventArgs
    {
        void HandleEvent(TEventArgs eventArgs);
    }
}

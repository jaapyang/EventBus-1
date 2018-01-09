namespace com.miaow.Core.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TRequest, TResponse> : IEventHandler
        where TRequest : IEventRequest
        where TResponse : IEventResponse, new()
    {
        TResponse HandleEvent(TRequest request);
    }
}

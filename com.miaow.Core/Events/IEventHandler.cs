namespace com.miaow.Core.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in TRequest, out TResponse> : IEventHandler
        where TRequest : IEventRequest
        where TResponse : IEventResponse, new()
    {
        TResponse HandleEvent(TRequest request);
    }
}

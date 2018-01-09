namespace com.miaow.Core.Events
{
    public abstract class EventHandlerBase<TEventRequest, TEventResponse> : IEventHandler<TEventRequest, TEventResponse>
        where TEventRequest : IEventRequest
        where TEventResponse : IEventResponse, new()
    {
        public TEventResponse HandleEvent(TEventRequest request)
        {
            var response = HandleProcess(request);
            return response;
        }

        protected abstract TEventResponse HandleProcess(TEventRequest request);
    }
}
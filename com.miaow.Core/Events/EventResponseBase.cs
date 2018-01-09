namespace com.miaow.Core.Events
{
    public abstract class EventResponseBase : EventDataBase, IEventResponse
    {
        protected EventResponseBase()
        {
            Status = ResponseStatus.Success;
        }

        public ResponseStatus Status { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Error
    }
}
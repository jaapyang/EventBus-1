namespace com.miaow.Core.Events
{
    public interface IEventResponse : IEventData
    {
        ResponseStatus Status { get; }
    }
}
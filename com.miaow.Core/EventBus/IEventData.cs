using System;

namespace com.miaow.Core.EventBus
{
    public interface IEventData
    {
        DateTime MessageTime { get; }
    }
}
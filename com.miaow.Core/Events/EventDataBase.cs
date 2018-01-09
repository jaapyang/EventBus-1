using System;

namespace com.miaow.Core.Events
{
    public abstract class EventDataBase:IEventData
    {
        public DateTime EventTime { get; }

        protected EventDataBase()
        {
            EventTime = DateTime.Now;
        }
    }
}
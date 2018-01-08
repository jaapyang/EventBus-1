using System;

namespace com.miaow.Core.EventBus
{
    public class ActionEventHandler<TEventArgs> : EventHandlerBase<TEventArgs> where TEventArgs : IEventArgs
    {
        public ActionEventHandler(Action<TEventArgs> action)
        {
            Action = action;
        }

        public Action<TEventArgs> Action { get; private set; }

        protected override void HandleProcess(TEventArgs eventArgs)
        {
            Action(eventArgs);
        }
    }
}
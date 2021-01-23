namespace Gameknit
{
    public abstract class BaseEventHandler<TEvent> : EventHandler where TEvent : IEvent
    {
        protected sealed override bool MatchesEvent(IEvent inputEvent)
        {
            if (inputEvent is TEvent @event)
            {
                return this.MatchesEvent(@event);
            }

            return false;
        }

        protected virtual bool MatchesEvent(TEvent inputEvent)
        {
            return true;
        }

        protected sealed override IEvent OnHandleEvent(IEvent inputEvent)
        {
            return this.OnHandleEvent((TEvent) inputEvent);
        }

        protected abstract TEvent OnHandleEvent(TEvent inputEvent);
    }
}
namespace Gameknit
{
    public abstract class BaseEventPipe<E> : EventPipe where E : IEvent
    {
        protected override bool MatchesEvent(IEvent inputEvent)
        {
            return inputEvent is E;
        }

        public virtual E PushEvent(E inputEvent)
        {
            return (E) this.PushEvent((IEvent) inputEvent);
        }
    }
}
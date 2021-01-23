using System.Collections.Generic;
using System.Linq;

namespace Gameknit
{
    public abstract class BaseEventPipedHandler<TEventPipeId, TEvent> :
        BaseEventHandler<TEvent>,
        IPipedEventHandler<TEventPipeId>
        where TEvent : IEvent
    {
        private readonly Dictionary<TEventPipeId, IEventPipe> pipeMap;

        protected BaseEventPipedHandler()
        {
            this.pipeMap = new Dictionary<TEventPipeId, IEventPipe>();
        }

        public virtual void BindPipe(IEventPipe eventPipe)
        {
            var id = this.GetIdFrom(eventPipe);
            this.pipeMap[id] = eventPipe;
        }

        public virtual void UnbindPipe(IEventPipe eventPipe)
        {
            var id = this.GetIdFrom(eventPipe);
            this.pipeMap.Remove(id);
        }

        protected sealed override TEvent OnHandleEvent(TEvent inputEvent)
        {
            var pipes = this.pipeMap.Values.ToList();
            foreach (var pipe in pipes)
            {
                pipe.PushEvent(inputEvent);
            }

            return this.OnHandleEvent(this, inputEvent);
        }

        protected virtual TEvent OnHandleEvent(
            BaseEventPipedHandler<TEventPipeId, TEvent> self,
            TEvent inputEvent
        )
        {
            return inputEvent;
        }

        public T GetPipe<T>(TEventPipeId id) where T : IEventPipe
        {
            return (T) this.pipeMap[id];
        }

        public IEnumerable<T> GetPipes<T>() where T : IEventPipe
        {
            return this.pipeMap.Values.OfType<T>();
        }

        protected abstract TEventPipeId GetIdFrom(IEventPipe pipe);
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Gameknit
{
    public abstract class EventPipe : MonoBehaviour, IEventPipe
    {
        public List<IEventHandler> handlerSequence { get; }

        protected EventPipe()
        {
            this.handlerSequence = new List<IEventHandler>();
        }

        #region PushEvent

        protected abstract bool MatchesEvent(IEvent inputEvent);

        public IEvent PushEvent(IEvent inputEvent)
        {
            if (!this.MatchesEvent(inputEvent))
            {
                return inputEvent;
            }

            var index = 0;
            while (index < this.handlerSequence.Count)
            {
                var handler = this.handlerSequence[index++];
                if (!handler.isEnabled)
                {
                    continue;
                }

                inputEvent = handler.HandleEvent(inputEvent);
                if (inputEvent == null)
                {
                    return this.GetDefaultEvent();
                }
            }

            return inputEvent;
        }

        protected virtual IEvent GetDefaultEvent()
        {
            return null;
        }

        #endregion
    }
}
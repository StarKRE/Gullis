using System;
using System.Collections.Generic;
using System.Linq;
using GameNode;
using Gameknit;
using UnityEngine;

namespace Gameknit
{
    public abstract class EventPipeline : MonoBehaviour, IEventPipeline
    {
        #region Event

        public event Action<IEvent> OnEventPushedEvent;

        #endregion

        private readonly Dictionary<Type, IEventPipe> pipeMap;

        protected EventPipeline()
        {
            this.pipeMap = new Dictionary<Type, IEventPipe>();
        }

        protected IEnumerable<IEventPipe> pipes
        {
            get { return this.pipeMap.Values; }
        }

        public virtual void AddPipe(IEventPipe pipe)
        {
            this.pipeMap.Add(pipe.GetType(), pipe);
        }

        public virtual void RemovePipe(IEventPipe pipe)
        {
            this.pipeMap.Remove(pipe.GetType());
        }

        public T GetPipe<T>() where T : IEventPipe
        {
            return DictionaryHelper.Find<T, IEventPipe>(this.pipeMap);
        }

        public IEnumerable<T> GetPipes<T>() where T : IEventPipe
        {
            return this.pipeMap.Values.OfType<T>();
        }

        public virtual void PushEvent(IEvent inputEvent)
        {
            foreach (var pipe in this.pipes)
            {
                pipe.PushEvent(inputEvent);
            }
        }

        public void NotifyAboutEventPushed(IEvent @event)
        {
            this.OnEventPushedEvent?.Invoke(@event);
        }
    }
}
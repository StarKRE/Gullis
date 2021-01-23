using System;
using System.Collections.Generic;

namespace Gameknit
{
    public interface IEventPipeline
    {
        event Action<IEvent> OnEventPushedEvent;
        
        void AddPipe(IEventPipe pipe);

        void RemovePipe(IEventPipe pipe);
        
        T GetPipe<T>() where T : IEventPipe;

        IEnumerable<T> GetPipes<T>() where T : IEventPipe;

        void PushEvent(IEvent inputEvent);

        void NotifyAboutEventPushed(IEvent @event);
    }
}
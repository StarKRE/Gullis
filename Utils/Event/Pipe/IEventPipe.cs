using System.Collections.Generic;

namespace Gameknit
{
    public interface IEventPipe
    {
        IEvent PushEvent(IEvent inputEvent);
        
        List<IEventHandler> handlerSequence { get; }
    }
}
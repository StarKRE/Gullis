using System.Collections.Generic;

namespace Gameknit
{
    public interface IPipedEventHandler<in TEventPipeId> : IEventHandler
    {
        T GetPipe<T>(TEventPipeId id) where T : IEventPipe;

        IEnumerable<T> GetPipes<T>() where T : IEventPipe;

        void UnbindPipe(IEventPipe eventPipe);

        void BindPipe(IEventPipe eventPipe);
    }
}
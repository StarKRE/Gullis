using System.Collections.Generic;

namespace Gameknit
{
    public static class EventPipeExtenstions
    {
        public static IEnumerable<T> GetHandlers<T>(this IEventPipe pipe) where T : IEventHandler
        {
            var requiredHandlers = new HashSet<T>();
            var handlerSequence = pipe.handlerSequence;
            foreach (var handler in handlerSequence)
            {
                if (handler is T requiredHandler)
                {
                    requiredHandlers.Add(requiredHandler);
                }
            }

            return requiredHandlers;
        }
    }
}
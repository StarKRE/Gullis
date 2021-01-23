namespace Gameknit
{
    public interface IEventHandler
    {
        bool isEnabled { get; set; }

        IEvent HandleEvent(IEvent inputEvent);
    }
}
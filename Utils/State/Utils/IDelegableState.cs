namespace Gameknit
{
    public interface IDelegableState : IState 
    {
        void OnProvideParent(object parent);
    }
}
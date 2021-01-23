namespace Gameknit
{
    public abstract class BaseMonoState : MonoState, IDelegableState
    {
        protected object parent { get; private set; }

        public void OnProvideParent(object parent)
        {
            this.parent = parent;
            this.OnParentProvided();
        }

        protected virtual void OnParentProvided()
        {
        }
    }
}
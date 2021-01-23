namespace Gameknit
{
    public abstract class BaseMonoStateMachine : MonoStateMachine, IDelegableState
    {
        protected object parent { get; private set; }

        public void OnProvideParent(object parent)
        {
            this.parent = parent;
            this.OnParentProvided();
        }

        protected virtual void OnParentProvided()
        {
            var states = this.GetStates<IDelegableState>();
            foreach (var state in states)
            {
                state.OnProvideParent(this.parent);
            }
        }
    }
}
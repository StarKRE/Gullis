namespace Gameknit
{
    public static class StateExtensions
    {
        #region In

        public static bool InState<T>(this IStateMachine stateMachine, out T state) where T : IState
        {
            state = stateMachine.GetState<T>();
            var equals = state.Equals(stateMachine.GetCurrentState<IState>());
            if (!equals)
            {
                state = default;
            }

            return equals;
        }

        #endregion
    }
}
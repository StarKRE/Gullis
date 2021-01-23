using System;

namespace Gameknit
{
    public interface IState
    {
        #region Event

        event Action<IState, IStateTransition> OnEnterEvent;

        event Action<IState> OnUpdateEvent;

        event Action<IState> OnExitEvent;

        #endregion

        void OnEnter(IStateTransition inputTransition);

        void OnUpdate();

        void OnExit();
    }
}
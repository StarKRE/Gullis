using System;
using UnityEngine;

namespace Gameknit
{
    public abstract class MonoState : MonoBehaviour, IState
    {
        #region Event

        public virtual event Action<IState, IStateTransition> OnEnterEvent;

        public virtual event Action<IState> OnUpdateEvent;

        public virtual event Action<IState> OnExitEvent;

        #endregion

        #region Lifecycle

        public virtual void OnEnter(IStateTransition inputTransition)
        {
            this.OnEnterEvent?.Invoke(this, inputTransition);
        }

        public virtual void OnUpdate()
        {
            this.OnUpdateEvent?.Invoke(this);
        }

        public virtual void OnExit()
        {
            this.OnExitEvent?.Invoke(this);
        }

        #endregion
    }
}
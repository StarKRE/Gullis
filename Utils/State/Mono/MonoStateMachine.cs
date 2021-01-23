using System;
using System.Collections.Generic;
using System.Linq;
using GameNode;
using UnityEngine;

namespace Gameknit
{
    public class MonoStateMachine : MonoState, IStateMachine
    {
        #region Event

        public override event Action<IState, IStateTransition> OnEnterEvent;

        public override event Action<IState> OnUpdateEvent;

        public override event Action<IState> OnExitEvent;
        
        public event Action<IState, IStateTransition> OnStateChangedEvent;

        #endregion

        protected IState currentState { get; set; }

        protected readonly Dictionary<Type, IState> stateMap;

        [SerializeField]
        private MonoStateMachineParams m_monoStateMachineParams
            = new MonoStateMachineParams();

        public MonoStateMachine()
        {
            this.stateMap = new Dictionary<Type, IState>();
        }

        protected virtual void Awake()
        {
            var states = this.m_monoStateMachineParams.m_states;
            foreach (var state in states)
            {
                var type = state.GetType();
                this.stateMap.Add(type, state);
            }

            var initialIndex = this.m_monoStateMachineParams.m_initialIndex;
            var initialState = states[initialIndex];
            var initialStateType = initialState.GetType();
            this.currentState = this.stateMap[initialStateType];
        }

        #region Lifecycle

        public override void OnEnter(IStateTransition inputTransition)
        {
            this.currentState.OnEnter(inputTransition);
            this.OnEnterEvent?.Invoke(this, inputTransition);
        }

        public override void OnUpdate()
        {
            this.currentState.OnUpdate();
            this.OnUpdateEvent?.Invoke(this);
        }

        public override void OnExit()
        {
            this.currentState.OnExit();
            this.OnExitEvent?.Invoke(this);
        }

        #endregion

        public T GetCurrentState<T>() where T : IState
        {
            return (T) this.currentState;
        }

        public virtual void SetCurrentState(IState nextState, IStateTransition transition = null)
        {
            var previousState = this.currentState;
            previousState.OnExit();
            this.currentState = nextState;
            nextState.OnEnter(transition);
            this.OnStateChangedEvent?.Invoke(this.currentState, transition);
        }

        public T GetState<T>() where T : IState
        {
            return DictionaryHelper.Find<T, IState>(this.stateMap);
        }

        public IEnumerable<T> GetStates<T>() where T : IState
        {
            return this.stateMap.Values.OfType<T>();
        }
        
        [Serializable]
        public sealed class MonoStateMachineParams
        {
            [SerializeField]
            public MonoState[] m_states;

            [SerializeField]
            public int m_initialIndex;
        }
    }
}
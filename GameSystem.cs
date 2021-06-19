using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameElements
{
    /// <summary>
    ///     <para>A gameplay system contract.</para>
    /// </summary>
    public interface IGameSystem : IGameObserver, IGameStateable, IGameElementLayer
    {
    }

    public class GameSystem : MonoBehaviour, IGameSystem
    {
        #region Event

        public event Action<object> OnGamePrepared;

        public event Action<object> OnGameReady;

        public event Action<object> OnGameStarted;

        public event Action<object> OnGamePaused;

        public event Action<object> OnGameResumed;

        public event Action<object> OnGameFinished;

        #endregion

        /// <summary>
        ///     2<para>A game state.</para>
        /// </summary>
        public GameState State { get; protected set; }

        private readonly Dictionary<Type, IGameElement> registeredElementMap;

        #region Lifecycle

        protected GameSystem()
        {
            this.State = GameState.CREATE;
            this.registeredElementMap = new Dictionary<Type, IGameElement>();
        }

        public void PrepareGame(object sender)
        {
            if (this.State != GameState.CREATE)
            {
                return;
            }

            this.State = GameState.PREPARE;
            foreach (var element in this)
            {
                element.OnPrepareGame(sender);
            }

            this.OnPrepareGame(sender);

            if (this.OnGamePrepared != null)
            {
                this.OnGamePrepared.Invoke(sender);
            }
        }

        protected virtual void OnPrepareGame(object sender)
        {
        }

        public void ReadyGame(object sender)
        {
            if (this.State != GameState.PREPARE)
            {
                return;
            }

            this.State = GameState.READY;
            foreach (var element in this)
            {
                element.OnReadyGame(sender);
            }

            this.OnReadyGame(this);

            if (this.OnGameReady != null)
            {
                this.OnGameReady.Invoke(sender);
            }
        }

        protected virtual void OnReadyGame(object sender)
        {
        }

        public void StartGame(object sender)
        {
            if (this.State != GameState.READY)
            {
                return;
            }

            this.State = GameState.PLAY;
            foreach (var element in this)
            {
                element.OnStartGame(sender);
            }
            
            this.OnStartGame(sender);

            if (this.OnGameStarted != null)
            {
                this.OnGameStarted.Invoke(sender);
            }
        }

        protected virtual void OnStartGame(object sender)
        {
        }

        public void PauseGame(object sender)
        {
            if (this.State == GameState.PAUSE)
            {
                return;
            }

            this.State = GameState.PAUSE;
            foreach (var element in this)
            {
                element.OnPauseGame(sender);
            }
            
            this.OnPauseGame(sender);

            if (this.OnGamePaused != null)
            {
                this.OnGamePaused.Invoke(sender);
            }
        }

        protected virtual void OnPauseGame(object sender)
        {
        }

        public void ResumeGame(object sender)
        {
            if (this.State != GameState.PAUSE)
            {
                return;
            }

            this.State = GameState.PLAY;
            foreach (var element in this)
            {
                element.OnResumeGame(sender);
            }
            
            this.OnResumeGame();

            if (this.OnGameResumed != null)
            {
                this.OnGameResumed.Invoke(sender);
            }
        }

        protected virtual void OnResumeGame()
        {
        }

        public void FinishGame(object sender)
        {
            if (this.State >= GameState.FINISH)
            {
                return;
            }

            this.State = GameState.FINISH;
            foreach (var element in this)
            {
                element.OnFinishGame(sender);
            }
            
            this.OnFinishGame(sender);

            if (this.OnGameFinished != null)
            {
                this.OnGameFinished.Invoke(sender);
            }
        }

        protected virtual void OnFinishGame(object sender)
        {
        }

        public void DestroyGame(object sender)
        {
            if (this.State != GameState.FINISH)
            {
                return;
            }

            this.State = GameState.DESTROY;
            foreach (var element in this)
            {
                element.OnDestroyGame(sender);
            }

            this.OnDestroyGame(sender);
        }

        protected virtual void OnDestroyGame(object sender)
        {
        }

        #endregion

        public bool RegisterElement(IGameElement element)
        {
            var type = element.GetType();
            if (this.registeredElementMap.ContainsKey(type))
            {
                return false;
            }

            this.registeredElementMap.Add(type, element);
            element.OnRegistered(this, this);
            GameElementUtils.SyncState(element, this.State, this);
            return true;
        }

        public bool UnregisterElement(IGameElement element)
        {
            var type = element.GetType();
            if (!this.registeredElementMap.Remove(type))
            {
                return false;
            }

            element.OnUnregistered();
            return true;
        }

        public T GetElement<T>()
        {
            return (T) GameElementUtils.Find<T, IGameElement>(this.registeredElementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            IGameElement result;
            if (GameElementUtils.TryFind(this.registeredElementMap, typeof(T), out result))
            {
                element = (T) result;
                return true;
            }

            element = default(T);
            return false;
        }

        public IEnumerable<IGameElement> GetElements()
        {
            return new HashSet<IGameElement>(this.registeredElementMap.Values);
        }

        public int GetElementCount()
        {
            return this.registeredElementMap.Count;
        }

        public IEnumerator<IGameElement> GetEnumerator()
        {
            foreach (var element in this.registeredElementMap.Values)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
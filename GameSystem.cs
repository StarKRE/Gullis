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
            
            this.OnGamePrepared?.Invoke(sender);
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
            
            this.OnGameReady?.Invoke(sender);
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
            GameElementUtils.UpdateElementState(element, this);
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

        public T GetElement<T>() where T : IGameElement
        {
            return GameElementUtils.Find<T, IGameElement>(this.registeredElementMap);
        }

        public bool TryGetElement<T>(out T element)
        {
            if (GameElementUtils.TryFind(this.registeredElementMap, typeof(T), out var result))
            {
                element = (T) result;
                return true;
            }

            element = default;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameknit
{
    /// <summary>
    ///     <para>Keeps unique game interfaces.</para>
    /// </summary>
    public interface IGameInterfaceSystem : IEnumerable<IGameInterface>
    {
        /// <summary>
        ///     <para>Adds an unique interface into this system.</para>
        /// </summary>
        /// <param name="gameInterface">An interface instance.</param>
        void RegisterInterface(IGameInterface gameInterface);

        /// <summary>
        ///     <para>Removes an unique interface from this system.</para>
        /// </summary>
        /// <param name="gameInterface">An interface instance.</param>
        void UnregisterInterface(IGameInterface gameInterface);

        /// <summary>
        ///     <para>Provides a game context reference.</para>
        /// </summary>
        /// <typeparam name="T">Game Context type.</typeparam>
        T ProvideGameContext<T>() where T : IGameContext;

        /// <summary>
        ///     <para>Returns registered interfaces.</para>
        /// </summary>
        IEnumerable<IGameInterface> GetInterfaces();

        /// <summary>
        ///     <para>Returs count of registered interfaces.</para>
        /// </summary>
        int GetInterfaceCount();
    }

    /// <inheritdoc cref="IGameInterfaceSystem"/>
    public abstract class GameInterfaceSystem : MonoBehaviour, IGameInterfaceSystem
    {
        public bool IsGameContextAttached
        {
            get { return !ReferenceEquals(this.attachedGameContext, null); }
        }

        /// <para>Bound game context reference.</para>
        private IGameContext attachedGameContext;

        protected HashSet<IGameInterface> RegisteredInterfaces { get; }

        public GameInterfaceSystem()
        {
            this.RegisteredInterfaces = new HashSet<IGameInterface>();
        }

        /// <inheritdoc cref="IGameInterfaceSystem.RegisterInterface"/>
        public virtual void RegisterInterface(IGameInterface gameInterface)
        {
            gameInterface.OnRegistered(this);
            this.RegisteredInterfaces.Add(gameInterface);
        }

        /// <inheritdoc cref="IGameInterfaceSystem.UnregisterInterface"/>
        public virtual void UnregisterInterface(IGameInterface gameInterface)
        {
            this.RegisteredInterfaces.Remove(gameInterface);
            gameInterface.OnUnregistered();
        }

        /// <summary>
        ///     <para>Binds a game system.</para>
        /// </summary>
        /// <param name="gameContext">Target game context.</param>
        public virtual void AttachContext(IGameContext gameContext)
        {
            if (this.IsGameContextAttached)
            {
                throw new Exception("Game context has already attached!");
            }

            this.attachedGameContext = gameContext;
            this.attachedGameContext.OnGamePreparedEvent += this.OnGamePrepared;
            if (this.attachedGameContext.Status >= GameStatus.PREPARING)
            {
                this.OnGamePrepared(this);
            }

            this.attachedGameContext.OnGameReadyEvent += this.OnGameReady;
            if (this.attachedGameContext.Status >= GameStatus.READY)
            {
                this.OnGameReady(this);
            }

            this.attachedGameContext.OnGameStartedEvent += this.OnGameStarted;
            if (this.attachedGameContext.Status >= GameStatus.PLAYING)
            {
                this.OnGameStarted(this);
            }

            this.attachedGameContext.OnGamePausedEvent += this.OnGamePaused;
            if (attachedGameContext.Status == GameStatus.PAUSING)
            {
                this.OnGamePaused(this);
            }

            this.attachedGameContext.OnGameResumedEvent += this.OnGameResumed;
            this.attachedGameContext.OnGameFinishedEvent += this.OnGameFinished;
        }

        /// <summary>
        ///     <para>Unbinds attached game context.</para>
        /// </summary>
        public void DetachContext()
        {
            if (!this.IsGameContextAttached)
            {
                return;
            }

            this.attachedGameContext.OnGamePreparedEvent -= this.OnGamePrepared;
            this.attachedGameContext.OnGameReadyEvent -= this.OnGameReady;
            this.attachedGameContext.OnGameStartedEvent -= this.OnGameStarted;
            this.attachedGameContext.OnGamePausedEvent -= this.OnGamePaused;
            this.attachedGameContext.OnGameResumedEvent -= this.OnGameResumed;
            this.attachedGameContext.OnGameFinishedEvent -= this.OnGameFinished;
            this.attachedGameContext = null;
        }

        #region GameCallbacks

        /// <inheritdoc cref="IGameContext.PrepareGame"/>
        protected virtual void OnGamePrepared(object sender)
        {
            foreach (var gameInterface in this)
            {
                gameInterface.OnGamePrepared(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.ReadyGame"/>
        protected virtual void OnGameReady(object sender)
        {
            foreach (var gameInterface in this)
            {
                gameInterface.OnGameReady(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.StartGame"/>
        protected virtual void OnGameStarted(object sender)
        {
            foreach (var gameInterface in this)
            {
                gameInterface.OnGameStarted(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.PauseGame"/>
        protected virtual void OnGamePaused(object sender)
        {
            foreach (var gameInterface in this)
            {
                gameInterface.OnGamePaused(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.ResumeGame"/>
        protected virtual void OnGameResumed(object sender)
        {
            foreach (var gameInterface in this)
            {
                gameInterface.OnGameResumed(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.FinishGame"/>
        protected virtual void OnGameFinished(object sender)
        {
            foreach (var gameInterface in this)
            {
                gameInterface.OnGameFinished(sender);
            }
        }

        #endregion

        /// <inheritdoc cref="IGameInterfaceSystem.ProvideGameContext{T}"/>
        public T ProvideGameContext<T>() where T : IGameContext
        {
            return (T) this.attachedGameContext;
        }

        /// <inheritdoc cref="IGameInterfaceSystem.GetInterfaces"/>
        public IEnumerable<IGameInterface> GetInterfaces()
        {
            return new HashSet<IGameInterface>(this.RegisteredInterfaces);
        }

        /// <inheritdoc cref="IGameInterfaceSystem.GetInterfaceCount"/>
        public int GetInterfaceCount()
        {
            return this.RegisteredInterfaces.Count;
        }
        
        public IEnumerator<IGameInterface> GetEnumerator()
        {
            foreach (var gameInterface in this.RegisteredInterfaces)
            {
                yield return gameInterface;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
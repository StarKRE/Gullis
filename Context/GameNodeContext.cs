using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameNode;
using UnityEngine;

namespace Gameknit
{
    /// <summary>
    ///     <inheritdoc cref="IGameContext"/>
    ///     <para>Node game system.</para>
    /// </summary>
    public interface IGameNodeContext : IGameContext, IGameNodeContainer, IGameNodeLayer
    {
    }

    public abstract class GameNodeContext : MonoBehaviour, IGameNodeContext
    {
        #region Event

        /// <inheritdoc cref="IGameContext.OnGamePreparedEvent"/>
        public abstract event Action<object> OnGamePreparedEvent;

        /// <inheritdoc cref="IGameContext.OnGameReadyEvent"/>
        public abstract event Action<object> OnGameReadyEvent;

        /// <inheritdoc cref="IGameContext.OnGameStartedEvent"/>
        public abstract event Action<object> OnGameStartedEvent;

        /// <inheritdoc cref="IGameContext.OnGamePausedEvent"/>
        public abstract event Action<object> OnGamePausedEvent;

        /// <inheritdoc cref="IGameContext.OnGameResumedEvent"/>
        public abstract event Action<object> OnGameResumedEvent;

        /// <inheritdoc cref="IGameContext.OnGameFinishedEvent"/>
        public abstract event Action<object> OnGameFinishedEvent;

        #endregion

        /// <inheritdoc cref="IGameContext.Status"/>
        public GameStatus Status { get; protected set; }

        /// <summary>
        ///     <para>Map of unique registered game components.
        ///     Key: a node type, Value: a node instance.</para>
        /// </summary>
        protected Dictionary<Type, IGameNode> RegisteredNodeMap { get; }

        #region Lifecycle

        protected GameNodeContext()
        {
            this.Status = GameStatus.CREATING;
            this.RegisteredNodeMap = new Dictionary<Type, IGameNode>();
        }

        /// <inheritdoc cref="IGameContext.PrepareGame"/>
        public virtual void PrepareGame(object sender)
        {
            this.Status = GameStatus.PREPARING;
            foreach (var node in this)
            {
                node.OnPrepareGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.ReadyGame"/>
        public virtual void ReadyGame(object sender)
        {
            this.Status = GameStatus.READY;
            foreach (var node in this)
            {
                node.OnReadyGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.StartGame"/>
        public virtual void StartGame(object sender)
        {
            this.Status = GameStatus.PLAYING;
            foreach (var node in this)
            {
                node.OnStartGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.PauseGame"/>
        public virtual void PauseGame(object sender)
        {
            this.Status = GameStatus.PAUSING;
            foreach (var node in this)
            {
                node.OnPauseGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.ResumeGame"/>
        public virtual void ResumeGame(object sender)
        {
            this.Status = GameStatus.PLAYING;
            foreach (var node in this)
            {
                node.OnResumeGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.FinishGame"/>
        public virtual void FinishGame(object sender)
        {
            this.Status = GameStatus.FINISHING;
            foreach (var node in this)
            {
                node.OnFinishGame(sender);
            }
        }

        /// <inheritdoc cref="IGameContext.DestroyGame"/>
        public virtual void DestroyGame(object sender)
        {
            this.Status = GameStatus.DESTROYING;
            foreach (var node in this)
            {
                node.OnDestroyGame(sender);
            }
        }

        #endregion

        /// <inheritdoc cref="IGameNodeLayer.RegisterNode"/>
        public virtual void RegisterNode(IGameNode gameNode)
        {
            var type = gameNode.GetType();
            this.RegisteredNodeMap.Add(type, gameNode);
            gameNode.OnRegistered(this, this);
        }

        /// <inheritdoc cref="IGameNodeLayer.UnregisterNode"/>
        public virtual void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnUnregistered();
            var type = gameNode.GetType();
            this.RegisteredNodeMap.Remove(type);
        }

        /// <inheritdoc cref="IGameNodeContainer.GetNodes"/>
        public IEnumerable<IGameNode> GetNodes()
        {
            return new HashSet<IGameNode>(this.RegisteredNodeMap.Values);
        }
        
        /// <inheritdoc cref="IGameNodeContainer.GetNodeCount"/>
        public int GetNodeCount()
        {
            return this.RegisteredNodeMap.Count;
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNode{T}"/>
        public T GetNode<T>() where T : IGameNode
        {
            return DictionaryHelper.Find<T, IGameNode>(this.RegisteredNodeMap);
        }

        /// <inheritdoc cref="IGameNodeLayer.TryGetNode{T}"/>
        public bool TryGetNode<T>(out T node) where T : IGameNode
        {
            var requiredType = typeof(T);
            if (DictionaryHelper.TryFind(this.RegisteredNodeMap, requiredType, out var result))
            {
                node = (T) result;
                return true;
            }

            node = default;
            return false;
        }

        /// <inheritdoc cref="IGameNodeLayer.GetNodes{T}"/>
        public IEnumerable<T> GetNodes<T>() where T : IGameNode
        {
            return this.RegisteredNodeMap.Values.OfType<T>();
        }

        public IEnumerator<IGameNode> GetEnumerator()
        {
            var nodes = this.RegisteredNodeMap.Values;
            foreach (var node in nodes)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
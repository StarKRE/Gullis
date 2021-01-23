using System.Collections;
using System.Collections.Generic;

namespace Gameknit
{
    /// <summary>
    ///     <para>Keeps nodes.</para>
    /// </summary>
    public interface IGameNodeContainer : IEnumerable<IGameNode>
    {
        /// <summary>
        ///     <para>Registers node into container.</para>
        /// </summary>
        /// <param name="gameNode">Registered node.</param>
        void RegisterNode(IGameNode gameNode);

        /// <summary>
        ///     <para>Unregisters node from container.</para>
        /// </summary>
        /// <param name="gameNode">Unregistered node.</param>
        void UnregisterNode(IGameNode gameNode);

        /// <summary>
        ///     <para>Returns registered nodes.</para>
        /// </summary>
        IEnumerable<IGameNode> GetNodes();

        /// <summary>
        ///     <para>Returns registered node count</para>
        /// </summary>
        int GetNodeCount();
    }
    
    public class GameNodeContainer : GameNode, IGameNodeContainer
    {
        protected HashSet<IGameNode> RegisteredNodes { get; }

        protected GameNodeContainer()
        {
            this.RegisteredNodes = new HashSet<IGameNode>();
        }

        /// <inheritdoc cref="IGameNode.OnPrepareGame"/>
        protected override void OnPrepareGame(object sender)
        {
            base.OnPrepareGame(sender);
            foreach (var node in this)
            {
                node.OnPrepareGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnReadyGame"/>
        protected override void OnReadyGame(object sender)
        {
            base.OnReadyGame(sender);
            foreach (var node in this)
            {
                node.OnReadyGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnStartGame"/>
        protected override void OnStartGame(object sender)
        {
            base.OnStartGame(sender);
            foreach (var node in this)
            {
                node.OnStartGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnPauseGame"/>
        protected override void OnPauseGame(object sender)
        {
            base.OnPauseGame(sender);
            foreach (var node in this)
            {
                node.OnPauseGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnResumeGame"/>
        protected override void OnResumeGame(object sender)
        {
            base.OnResumeGame(sender);
            foreach (var node in this)
            {
                node.OnResumeGame(sender);
            }
        }

        /// <inheritdoc cref="IGameNode.OnFinishGame"/>
        protected override void OnFinishGame(object sender)
        {
            base.OnFinishGame(sender);
            foreach (var node in this)
            {
                node.OnFinishGame(sender);
            }
        }

        protected override void OnDestroyGame(object sender)
        {
            foreach (var node in this)
            {
                node.OnDestroyGame(sender);
            }

            base.OnDestroyGame(sender);
        }

        /// <inheritdoc cref="IGameNodeLayer.RegisterNode"/>
        public virtual void RegisterNode(IGameNode gameNode)
        {
            this.RegisteredNodes.Add(gameNode);
            gameNode.OnRegistered(this, this.GameContext);
        }

        /// <inheritdoc cref="IGameNodeLayer.UnregisterNode"/>
        public virtual void UnregisterNode(IGameNode gameNode)
        {
            gameNode.OnUnregistered();
            this.RegisteredNodes.Remove(gameNode);
        }

        /// <inheritdoc cref="IGameNodeContainer.GetNodes"/>
        public IEnumerable<IGameNode> GetNodes()
        {
            return new HashSet<IGameNode>(this.RegisteredNodes);
        }

        /// <inheritdoc cref="IGameNodeContainer.GetNodeCount"/>
        public int GetNodeCount()
        {
            return this.RegisteredNodes.Count;
        }

        public IEnumerator<IGameNode> GetEnumerator()
        {
            foreach (var node in this.RegisteredNodes)
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